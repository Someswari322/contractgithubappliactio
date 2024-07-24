using Azure;
using Azure.Data.Tables;
using azureapplicationdemo.api.Entity;
using azureapplicationdemo.api.ViewModel;
using Microsoft.Graph.Models;

namespace azureapplicationdemo.api.Services.AzureTable
{
    public class AzurestorageTableservice : IAzurestorageTableservice
    {
        private TableClient _tableClient;
        private const string partionkey = "blog";
        public AzurestorageTableservice(TableServiceClient tableServiceClient) 
        {
            _tableClient = tableServiceClient.GetTableClient("blogdemo");
        
        }
        public async Task<List<BlogResponse>> GetAll()
        {
            var blogList = new List<BlogResponse>();
            var tableResult = _tableClient.QueryAsync<Blog>(filter: "");

           await foreach (var item in tableResult)
            {
                blogList.Add(new BlogResponse
                {
                    ID = item.RowKey,
                    Author=item.Author,
                    Name = item.Name,
                    Description = item.Description,
                });
            }
           return blogList;
        }

        public async Task<BlogResponse> GetbyId(string Id)
        {
            var blog = new BlogResponse();
            var blogEntity = await _tableClient.GetEntityAsync<Blog>(partionkey, Id);
            blog = new BlogResponse
            {
                ID = blogEntity.Value.RowKey,
                Author = blogEntity.Value.Author,
                Name = blogEntity.Value.Name,
                Description = blogEntity.Value.Description,
            };
            return blog;
        }

        public async Task<bool> Add(BlogResponse blog)
        {
            try
            {
                var blogEntity = new Blog
                {
                    RowKey = Guid.NewGuid().ToString(),
                    Author = blog.Author,
                    Name = blog.Name,
                    Description = blog.Description,
                    PartitionKey = partionkey
                };
                await _tableClient.CreateIfNotExistsAsync();
                var response = await _tableClient.AddEntityAsync<Blog>(blogEntity);
                return true;
            }
            catch(RequestFailedException reex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task<bool> Update(string id, BlogResponse blog)
        {
            var Currentblog = await _tableClient.GetEntityAsync<Blog>(partionkey, id);
            Currentblog.Value.Author = blog.Author;
            Currentblog.Value.Name = blog.Name;
            Currentblog.Value.Description = blog.Description;
            await _tableClient.UpdateEntityAsync<Blog>(Currentblog, Currentblog.Value.ETag);
            return true;

        }

        public async Task<bool> Delete(string id)
        {
            var blogEntity = await _tableClient.GetEntityAsync<Blog>(partionkey, id);
            await _tableClient.DeleteEntityAsync(blogEntity.Value.PartitionKey, blogEntity.Value.RowKey);
            return true;

        }

    }
}

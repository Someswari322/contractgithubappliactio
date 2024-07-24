using azureapplicationdemo.api.ViewModel;

namespace azureapplicationdemo.api.Services.AzureTable
{
    public interface IAzurestorageTableservice
    {
        Task<List<BlogResponse>> GetAll();

        Task <BlogResponse> GetbyId(string id);

        Task<bool> Add(BlogResponse blog);

        Task <bool> Delete(string id);

        Task<bool> Update(string id,BlogResponse blog);
    }
}

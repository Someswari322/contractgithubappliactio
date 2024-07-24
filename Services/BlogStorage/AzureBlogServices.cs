using Azure;
using Azure.Storage.Blobs;
using azureapplicationdemo.api.ViewModel;
using Microsoft.Azure.Storage.Blob.Protocol;

namespace azureapplicationdemo.api.Services.BlogStorage
{
    public class AzureBlogServices:IAzureBlogService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlogServices(BlobServiceClient blobServiceClient) 
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task<AzureBlobResponse> UploadFileAsync(
            string BlobContainer, string DirectoryName, string filename, Stream filestrem)
        {
           
                var blobResponse = new AzureBlobResponse();
            try
            {
                //create blob container
                var container = _blobServiceClient.GetBlobContainerClient(BlobContainer);
                await container.CreateIfNotExistsAsync();
                //folder1/folder2/fillename.pdf
                var blob = container.GetBlobClient($"{DirectoryName}/{filename}");
                var blobResult = await blob.UploadAsync(filestrem);

                blobResponse = new AzureBlobResponse()
                {
                    BlogUri = blob.Uri,
                    FileName = blob.Name,
                    StatusCode = blobResult.GetRawResponse().Status,
                    ReasonPhrase = blobResult.GetRawResponse().ReasonPhrase

                };
            }
            catch (RequestFailedException rfex)
            {
                blobResponse.IsError = true;
                blobResponse.ErrorMessage = rfex.Message;
                blobResponse.StatusCode = rfex.Status;
            }
            catch (Exception ex)
            {
                blobResponse.IsError = true;
                blobResponse.ErrorMessage = ex.Message;
                blobResponse.StatusCode = 500;
            }
            return blobResponse;
        }
    }
}

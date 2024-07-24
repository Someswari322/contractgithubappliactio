using azureapplicationdemo.api.ViewModel;

namespace azureapplicationdemo.api.Services.BlogStorage
{
    public interface IAzureBlogService
    {
        Task<AzureBlobResponse> UploadFileAsync(
            string BlobContainer, string DirectoryName,string filename,Stream filestrem);
    }
}

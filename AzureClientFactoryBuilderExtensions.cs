using Azure.Core.Extensions;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
namespace azureapplicationdemo.api
{
    internal static class AzureClientFactoryBuilderExtensions
    {
        public static IAzureClientBuilder<QueueServiceClient,QueueClientOptions>
            AddQueueServiceClient(this AzureClientFactoryBuilder builder,
            string serviceUriorConnectionString,bool preferMsi)
        {
            if(preferMsi && Uri.TryCreate(serviceUriorConnectionString,UriKind.Absolute,out Uri? serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
                return builder.AddQueueServiceClient(serviceUriorConnectionString);
            }
        }

        public static IAzureClientBuilder<TableServiceClient, TableClientOptions>
            AddTableServiceClient(this AzureClientFactoryBuilder builder,
            string serviceUriorConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriorConnectionString, UriKind.Absolute, out Uri? serviceUri))
            {
                return builder.AddTableServiceClient(serviceUri);
            }
            else
            {
                return builder.AddTableServiceClient(serviceUriorConnectionString);
            }
        }

        public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions>
            AddBlobServiceClient(this AzureClientFactoryBuilder builder,
            string serviceUriorConnectionString, bool preferMsi)
        {
            if (preferMsi && Uri.TryCreate(serviceUriorConnectionString, UriKind.Absolute, out Uri? serviceUri))
            {
                return builder.AddBlobServiceClient(serviceUri);
            }
            else
            {
                return builder.AddBlobServiceClient(serviceUriorConnectionString);
            }
        }

    }

}

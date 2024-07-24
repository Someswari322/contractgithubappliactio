using Azure;
using Azure.Data.Tables;

namespace azureapplicationdemo.api.Entity
{
    public class Blog : ITableEntity
    {
        public string PartitionKey { get ; set ; }
        public string RowKey { get ; set ; }
        public DateTimeOffset? Timestamp { get ; set ; }
        public ETag ETag { get ; set ; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
       
    }
}

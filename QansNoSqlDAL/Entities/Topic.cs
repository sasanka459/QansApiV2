using Azure;
using Azure.Data.Tables;

namespace QansNoSqlDAL.Entities
{
   

    public class Topic : ITableEntity
    {
        public string PartitionKey { get; set; }   // e.g., "azure"
        public string RowKey { get; set; }         // e.g., "az-net"
        public string ContentType { get; set; }    // "Topic" or "Chapter"
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentTopic { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

}

using Azure;
using Azure.Data.Tables;

namespace QansNoSqlDAL.Entities
{
    public class Exam : ITableEntity
    {
        public string PartitionKey { get; set; } = string.Empty;  // e.g., "azure"
        public string RowKey { get; set; } = string.Empty;        // e.g., "az-net"
        public string ExamCode { get; set; } = string.Empty;    // "Topic" or "Chapter"

        public string Topic { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public string ExamDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;

        public string CreatedBy { get; set; } = string.Empty;
        public int SortingOrder { get; set; }
        public string DetailsLink { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
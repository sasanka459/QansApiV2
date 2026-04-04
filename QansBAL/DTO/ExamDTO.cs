using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace QansBAL.DTO
{
    public class ExamDTO
    {
        public string PartitionKey { get; set; } = string.Empty;  // e.g., "azure"
        public string RowKey { get; set; } = string.Empty;        // e.g., "az-net"
        public string ExamCode { get; set; } = string.Empty;    // "Topic" or "Chapter"

        public string Topic { get; set; } = string.Empty;
        public string ExamName { get; set; } = string.Empty;
        public string ExamDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public int SortingOrder { get; set; }
        public string DetailsLink { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
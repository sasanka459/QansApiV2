using Azure;
using Azure.Data.Tables;

namespace QansNoSqlDAL.Entities
{
    public class Question : ITableEntity
    {
        public string PartitionKey {  get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string? QuestionHeader { get; set; }

        public string? QuestionBody { get; set; }

        public string? QuestionType { get; set; }

        public string? CreatedBy { get; set; }


    }
}

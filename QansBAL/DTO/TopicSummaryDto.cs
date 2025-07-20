using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.DTO
{
    public class TopicSummaryDto
    {
        public string PartitionKey {  get; set; }

        public string RowKey { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.DTO
{
   
    public class Topic
    {
        public string ContentType { get; set; }     // "Topic" or "Chapter"
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentTopic { get; set; }     // null for "Topic"
    }

    
}

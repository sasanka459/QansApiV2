using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansDAL.Entities
{
    public partial class Exam
    {
        public int ExamId { get; set; }              // Primary Key

        public string ExamName { get; set; } = null!; // AZ-900, AZ-204, etc.

        public string? Technology { get; set; }       // Azure, AWS, GCP

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        // Navigation Properties

        public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
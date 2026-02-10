using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QansBAL.DTO
{
    public class Question
    {
        public int Id { get; set; }
        public required string Topic { get; set; }

        public required string Subject {  get; set; }

        public string? QuestionHeader { get; set; }

        public string? QuestionBody { get; set; }

        public string? QuestionType { get; set; }

        public string? CreatedBy { get; set; }
    }
}

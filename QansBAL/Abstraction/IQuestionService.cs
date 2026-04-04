using QansBAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.Abstraction
{
    public interface IQuestionService
    {
        Task SaveQuestion(Question question);

        // Fix: Return a list and accept filter strings
        Task<IEnumerable<Question>> GetQuestions(string? topic, string? subject);
    }
}
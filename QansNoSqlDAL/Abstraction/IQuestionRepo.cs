using QansNoSqlDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansNoSqlDAL.Abstraction
{
    public interface IQuestionRepo
    {
        Task SaveQuestion(Question question);

        Task<Question> GetQuestion(string partitionKey, string rowKey);

        Task<List<Question>> GetAll(string? partitionKey = null);
    }
}
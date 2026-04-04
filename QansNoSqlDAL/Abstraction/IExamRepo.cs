using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using QansNoSqlDAL.Entities;

namespace QansNoSqlDAL.Abstraction
{
    public interface IExamRepo
    {
        public Task SaveExam(Exam exam);

        public Task<List<Exam>> GetAllExams();

        public Task<Exam?> GetExam(string partitionKey, string rowKey);

        public Task UpdateExam(Exam exam);
    }
}
using Azure.Data.Tables;
using QansNoSqlDAL.Abstraction;
using QansNoSqlDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansNoSqlDAL.Services
{
    public class QuestionRepoService : IQuestionRepo
    {
        private readonly TableClient _questionTable;
        public QuestionRepoService(TableServiceClient tableServiceClient)
        {
            _questionTable = tableServiceClient.GetTableClient("tblQans");
            _questionTable.CreateIfNotExists();
        }

        public async Task SaveQuestion(Question question)
        {
           await _questionTable.AddEntityAsync(question);
        }
    }
}

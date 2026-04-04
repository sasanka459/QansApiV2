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

        // --- NEW FETCH METHODS ---

        public async Task<Question> GetQuestion(string partitionKey, string rowKey)
        {
            try
            {
                var response = await _questionTable.GetEntityAsync<Question>(partitionKey, rowKey);
                return response.Value;
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 404)
            {
                return null; // Return null if not found
            }
        }

        public async Task<List<Question>> GetAll(string? topicKey = null)
        {
            var questions = new List<Question>();

            // If topicKey is provided, we filter by PartitionKey for high performance
            string filter = string.IsNullOrEmpty(topicKey)
                ? null
                : TableClient.CreateQueryFilter($"PartitionKey eq {topicKey}");

            var queryResults = _questionTable.QueryAsync<Question>(filter);

            await foreach (var question in queryResults)
            {
                questions.Add(question);
            }

            return questions;
        }
    }
}
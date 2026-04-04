using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using QansNoSqlDAL.Abstraction;
using QansNoSqlDAL.Entities;

namespace QansNoSqlDAL.Services
{
    public class ExamRepoService : IExamRepo
    {
        private readonly TableClient _questionTable;

        public ExamRepoService(TableServiceClient tableServiceClient)
        {
            _questionTable = tableServiceClient.GetTableClient("tblExams");
            _questionTable.CreateIfNotExists();
        }

        public async Task SaveExam(Exam exam)
        {
            await _questionTable.AddEntityAsync(exam);
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<Exam>> GetAllExams()
        {
            var results = new List<Exam>();

            await foreach (var exam in _questionTable.QueryAsync<Exam>())
            {
                results.Add(exam);
            }

            return results;
        }

        public async Task<Exam?> GetExam(string partitionKey, string rowKey)
        {
            try
            {
                var response = await _questionTable.GetEntityAsync<Exam>(partitionKey, rowKey);
                return response.Value;
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 404)
            {
                return null; // Not found → OK for create
            }
        }

        public async Task UpdateExam(Exam exam)
        {
            // ETag.All forces the update even if data changed since last read (Last-Write-Wins)
            // TableUpdateMode.Replace completely replaces the entity properties
            await _questionTable.UpdateEntityAsync(exam, ETag.All, TableUpdateMode.Replace);
        }
    }
}
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QansBAL.Abstraction;
using QansBAL.DTO;
using QansNoSqlDAL.Abstraction;
using et = QansNoSqlDAL.Entities;

namespace QansBAL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _questionRepo;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(IQuestionRepo questionRepo, ILogger<QuestionService> logger)
        {
            _questionRepo = questionRepo;
            _logger = logger;
        }

        public async Task SaveQuestion(Question question)
        {
            string? serializedMetadata = question.Metadata != null
                ? JsonSerializer.Serialize(question.Metadata)
                : null;

            et.Question qus = new et.Question()
            {
                PartitionKey = $"{question.Topic}|{question.Subject}",
                RowKey = Guid.NewGuid().ToString(),
                QuestionHeader = question.QuestionHeader,
                QuestionBody = question.QuestionBody,
                QuestionType = question.QuestionType,
                CreatedBy = question.CreatedBy,
                MetadataJson = serializedMetadata
            };

            await _questionRepo.SaveQuestion(qus);
            _logger.LogInformation("Question saved with PartitionKey: {PartitionKey} and RowKey: {RowKey}", qus.PartitionKey, qus.RowKey);
        }

        public async Task<IEnumerable<Question>> GetQuestions(string? topic, string? subject)
        {
            // 1. Build the composite PartitionKey
            string? partitionKeyFilter = (topic != null && subject != null)
                                         ? $"{topic}|{subject}"
                                         : null;

            // 2. Fetch from Repo
            var entities = await _questionRepo.GetAll(partitionKeyFilter);

            // 3. Map Entities back to DTOs
            return entities.Select(e =>
            {
                // Explicitly handle the JSON string to avoid compiler confusion
                string? jsonString = e.MetadataJson?.ToString();

                return new Question
                {
                    QuestionHeader = e.QuestionHeader,
                    QuestionBody = e.QuestionBody,
                    QuestionType = e.QuestionType,
                    CreatedBy = e.CreatedBy,
                    Topic = topic ?? (e.PartitionKey?.Split('|').FirstOrDefault()),
                    Subject = subject ?? (e.PartitionKey?.Split('|').LastOrDefault()),

                    // Fix: Specify <object> or <JsonElement> to avoid Stream overload issues
                    Metadata = !string.IsNullOrEmpty(jsonString)
                               ? JsonSerializer.Deserialize<object>(jsonString)
                               : null
                };
            }).ToList(); // Materialize the list
        }
    }
}
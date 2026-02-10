using QansBAL.Abstraction;
using QansBAL.DTO;
using QansNoSqlDAL.Abstraction;
using et=QansNoSqlDAL.Entities;

namespace QansBAL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionService(IQuestionRepo questionRepo)
        {
                _questionRepo = questionRepo;
        }
        public async Task SaveQuestion(Question question)
        {
            //Map DTO to entity
            et.Question qus = new et.Question()
            {
                PartitionKey = $"{question.Topic}|{question.Subject}",
                RowKey = Guid.NewGuid().ToString(),
                QuestionHeader = question.QuestionHeader,
                QuestionBody = question.QuestionBody.ToString(),
                QuestionType = question.QuestionType,
                CreatedBy = question.CreatedBy
            };

          await  _questionRepo.SaveQuestion(qus);

        }
    }
}

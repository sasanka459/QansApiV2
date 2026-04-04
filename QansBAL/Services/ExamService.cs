using QansBAL.Abstraction;
using QansBAL.DTO;
using QansDAL.Entities;
using QansNoSqlDAL.Abstraction;
using QansNoSqlDAL.Entities;

using et = QansNoSqlDAL.Entities;

namespace QansBAL.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepo _examRepo;

        public ExamService(IExamRepo examRepo)
        {
            _examRepo = examRepo;
        }

        public async Task SaveExam(ExamDTO exam)
        {
            ValidateExam(exam);
            var existingExam = await _examRepo.GetExam(exam.Topic, exam.ExamCode);

            if (existingExam != null)
                throw new Exception($"Exam {exam.ExamCode} already exists for Topic {exam.Topic}");
            // Map DTO → Table Entity
            et.Exam examEntity = new et.Exam()
            {
                PartitionKey = $"{exam.Topic}",  // Example grouping
                RowKey = $"{exam.ExamCode}",

                ExamName = exam.ExamName,
                Topic = exam.Topic,
                ExamCode = exam.ExamCode,
                SortingOrder = exam.SortingOrder,
                DetailsLink = exam.DetailsLink,
                ExamDescription = exam.ExamDescription,
                CreatedBy = exam.CreatedBy,
                IsActive = exam.IsActive,
                UpdatedBy = exam.UpdatedBy,
                UpdatedDate = exam.UpdatedDate,
            };

            await _examRepo.SaveExam(examEntity);
        }

        // =========================
        // GET ALL EXAMS
        // =========================
        public async Task<List<ExamDTO>> GetAllExams()
        {
            var entities = await _examRepo.GetAllExams();

            return entities.Select(x => new ExamDTO
            {
                PartitionKey = x.PartitionKey,
                RowKey = x.RowKey,
                ExamName = x.ExamName,
                Topic = x.Topic,
                ExamCode = x.ExamCode,
                SortingOrder = x.SortingOrder,
                DetailsLink = x.DetailsLink,
                ExamDescription = x.ExamDescription,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate
            }).ToList();
        }

        public void ValidateExam(ExamDTO exam)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            if (string.IsNullOrWhiteSpace(exam.Topic))
                throw new Exception("Topic is required");

            if (string.IsNullOrWhiteSpace(exam.ExamCode))
                throw new Exception("Exam Code is required");

            if (string.IsNullOrWhiteSpace(exam.ExamName))
                throw new Exception("Exam Name is required");
        }

        public async Task UpdateExam(ExamDTO examDto)
        {
            ValidateExam(examDto);

            // 2. Check if it actually exists before updating (Optional but recommended)
            var existingExam = await _examRepo.GetExam(examDto.PartitionKey, examDto.RowKey);
            if (existingExam == null)
            {
                throw new Exception($"Exam {examDto.ExamCode} not found.");
            }

            // 3. Map DTO -> Entity
            // Note: PartitionKey and RowKey MUST match the original to update the correct record
            var examEntity = new QansNoSqlDAL.Entities.Exam
            {
                PartitionKey = examDto.Topic,       // PK
                RowKey = examDto.RowKey,            // RK (ensure this is passed from frontend!)

                ExamCode = examDto.ExamCode,
                SortingOrder = examDto.SortingOrder,
                Topic = examDto.Topic,
                ExamName = examDto.ExamName,
                ExamDescription = examDto.ExamDescription,
                IsActive = examDto.IsActive,        // Ensure casing matches your Entity

                CreatedBy = existingExam.CreatedBy, // Preserve original creator
                CreatedDate = existingExam.CreatedDate, // Preserve original date

                UpdatedBy = "System",               // Or pass current user
                UpdatedDate = DateTime.UtcNow       // Set new update time
            };

            // 4. Save
            await _examRepo.UpdateExam(examEntity);
        }
    }
}
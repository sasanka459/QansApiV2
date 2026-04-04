using QansBAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.Abstraction
{
    public interface IExamService
    {
        /// <summary>
        /// Transform the DTO model into repo entity.
        /// Calls the repo layer to save it in the table storage.
        /// </summary>
        /// <param name="exam">DTO exam</param>
        /// <returns></returns>
        public Task SaveExam(ExamDTO exam);

        public Task<List<ExamDTO>> GetAllExams();

        public void ValidateExam(ExamDTO exam);

        public Task UpdateExam(ExamDTO exam);
    }
}
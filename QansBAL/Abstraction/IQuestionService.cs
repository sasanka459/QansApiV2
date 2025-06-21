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
        /// <summary>
        /// Transform the DTO model into repo entity.
        /// Calls the repo layer to save it in the table storage.
        /// </summary>
        /// <param name="question">DTO Question</param>
        /// <returns></returns>
        public Task SaveQuestion(Question question);
    }
}

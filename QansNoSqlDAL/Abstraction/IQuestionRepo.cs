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
        public Task SaveQuestion(Question question);
    }
}

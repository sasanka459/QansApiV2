using QansDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansDAL.Abstraction
{
    public interface IUserRepo
    {
        public IEnumerable<User> GetUser();
    }
}

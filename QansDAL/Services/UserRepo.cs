using Microsoft.EntityFrameworkCore;
using QansDAL.Abstraction;
using QansDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansDAL.Services
{
    public class UserRepo:IUserRepo
    {
        private readonly QansDbContext _context;
        public UserRepo(QansDbContext qansDbContext)
        {
            _context = qansDbContext;
        }
        public async Task<IEnumerable<User>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

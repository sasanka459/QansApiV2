using QansBAL.Abstraction;
using QansBAL.Models;
using QansDAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QansBAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo iUserRepo)
        {
            _userRepo = iUserRepo;
        }
        public async Task<IEnumerable<User>> GetUser()
        {
            var users =await _userRepo.GetUser();
            return  users.Select(x => new User
            {
                UserId = x.UserId,
                Username = x.Username,
                PasswordHash = x.PasswordHash,
                MobileNo = x.MobileNo,
                CreatedAt = x.CreatedAt,
                Email = x.Email
            });
        }
    }
}

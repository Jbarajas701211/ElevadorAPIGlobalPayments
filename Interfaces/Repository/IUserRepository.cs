using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserForEmailAsync(string email);
        Task<bool> UpdateUserBlockedAsync(User user);
        Task<bool> UpdateAttemptAsync(UserAttempts userAttempts);
        Task<bool> CreateAttemptAsync(UserAttempts userAttempts);
        Task<bool> CreateUserAsync(User user);
        Task<UserAttempts?> GetAttemptsUserAsync(int userId);
    }
}

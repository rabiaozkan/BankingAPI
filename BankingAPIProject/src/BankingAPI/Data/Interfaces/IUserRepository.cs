using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
    }
}

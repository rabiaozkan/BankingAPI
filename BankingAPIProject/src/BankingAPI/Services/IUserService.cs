using BankingAPI.DTOs;
using BankingAPI.Models;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(UserRegistrationDto registrationDto);
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto userUpdateDto);
        Task<bool> UpdateUserRoleAsync(int userId, UserRoleUpdateDto roleUpdateDto);
        Task<string> GenerateJwtTokenAsync(User user);
    }
}

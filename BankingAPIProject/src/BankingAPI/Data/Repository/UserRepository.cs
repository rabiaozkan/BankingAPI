using BankingAPI.Data.Interfaces;
using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankingContext _context;

        public UserRepository(BankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Username == username));
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            // Diğer güncellenebilir alanların atamaları

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await Task.FromResult(_context.Users.ToList());
            }

            return await Task.FromResult(_context.Users
                .Where(u => u.Username.Contains(searchTerm) || u.Email.Contains(searchTerm))
                .ToList());
        }
    }
}
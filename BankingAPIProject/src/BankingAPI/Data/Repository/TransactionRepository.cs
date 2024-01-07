using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankingAPI.Models;
using BankingAPI.Data.Interfaces;

namespace BankingAPI.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _context;

        public TransactionRepository(BankingContext context)
        {
            _context = context;
        }

        // İşlem ekleme
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        // İşlemi ID'ye göre getirme
        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.FindAsync(transactionId);
        }

        // Hesap ID'ye göre işlemleri getirme
        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _context.Transactions
                                 .Where(t => t.AccountId == accountId)
                                 .ToListAsync();
        }

        // İşlem güncelleme
        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        // İşlem silme
        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

    }
}

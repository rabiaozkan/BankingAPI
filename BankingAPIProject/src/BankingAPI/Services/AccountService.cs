using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Data;

namespace BankingAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankingContext _context;
        private readonly decimal _minimumOpeningBalance = 100;

        public AccountService(BankingContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(AccountCreationDto accountCreationDto)
        {
            if (accountCreationDto.InitialDeposit < _minimumOpeningBalance)
            {
                throw new Exception($"Minimum opening balance should be at least {_minimumOpeningBalance}");
            }

            var newAccount = new Account
            {
                UserId = accountCreationDto.UserId,
                Balance = accountCreationDto.InitialDeposit,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                AccountType = accountCreationDto.AccountType,
            };

            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            return newAccount;
        }

        public async Task<decimal?> GetBalanceAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            return account?.Balance;
        }

        public async Task<bool> UpdateBalanceAsync(int accountId, BalanceUpdateDto balanceUpdateDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var account = _context.Accounts.Find(accountId);
                    if (account == null) throw new Exception("Account not found");

                    if (account.Balance + balanceUpdateDto.Amount < 0)
                    {
                        throw new Exception("Insufficient funds for this operation");
                    }

                    account.Balance += balanceUpdateDto.Amount;
                    _context.SaveChanges();

                    transaction.Commit(); // İşlem başarılı ise commit yapılır
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Hata oluşursa işlemler geri alınır
                    throw;
                }
            }
        }

        public async Task<bool> DepositAsync(TransactionDto transactionDto)
        {
            // Para yatırma işlemini gerçekleştirecek kodlar
            var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
            
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.Balance += transactionDto.Amount;
            _context.SaveChanges();
            return true;
        }

        // Para çekme işlemini gerçekleştirecek kodlar
        public async Task<bool> WithdrawAsync(TransactionDto transactionDto)
        {
            var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if (account.Balance < transactionDto.Amount)
            {
                throw new Exception("Insufficient funds");
            }

            account.Balance -= transactionDto.Amount;
            _context.SaveChanges();
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankingContext _context;

        public TransactionService(BankingContext context)
        {
            _context = context;
        }

        public async Task<bool> DepositAsync(TransactionDto transactionDto)
        {
            var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.Balance += transactionDto.Amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = transactionDto.AccountId,
                Amount = transactionDto.Amount,
                TransactionDate = DateTime.UtcNow,
                Type = "deposit",
                Status = "completed",
            });

            await _context.SaveChangesAsync();
            return true;
        }

        // Para çekme işlemleri
        public async Task<bool> WithdrawAsync(TransactionDto transactionDto)
        {
            var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
            if (account == null || account.Balance < transactionDto.Amount)
            {
                throw new Exception("Invalid transaction");
            }

            account.Balance -= transactionDto.Amount;
            _context.Transactions.Add(new Transaction
            {
                AccountId = transactionDto.AccountId,
                Amount = transactionDto.Amount,
                TransactionDate = DateTime.UtcNow,
                Type = "withdrawal",
                Status = "completed",
            });

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> TransferAsync(TransferDto transferDetails)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await ValidateTransferDetailsAsync(transferDetails);
                    await UpdateAccountBalancesAsync(transferDetails);
                    await RecordTransferTransactionAsync(transferDetails);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await LogErrorAsync(ex);
                    throw;
                }
            }
        }

        private async Task ValidateTransferDetailsAsync(TransferDto transferDetails)
        {
            // Gönderici hesabın varlığını kontrol et.
            var fromAccount = await _context.Accounts.FindAsync(transferDetails.FromAccountId); 
            if (fromAccount == null)
            {
                throw new Exception("Gönderici hesap bulunamadı.");
            }

            // Alıcı hesabın varlığını kontrol et.
            var toAccount = await _context.Accounts.FindAsync(transferDetails.ToAccountId);
            if (toAccount == null)
            {
                throw new Exception("Alıcı hesap bulunamadı.");
            }

            // Gönderici hesabın yeterli bakiyesi olup olmadığını kontrol et.
            if (fromAccount.Balance < transferDetails.Amount)
            {
                throw new Exception("Yetersiz bakiye.");
            }
        }

        private async Task UpdateAccountBalancesAsync(TransferDto transferDetails)
        {
            var fromAccount = await _context.Accounts.FindAsync(transferDetails.FromAccountId);
            var toAccount = await _context.Accounts.FindAsync(transferDetails.ToAccountId);

            // Gönderici hesaptan miktarı çıkar.
            fromAccount.Balance -= transferDetails.Amount;

            // Alıcı hesaba miktarı ekle.
            toAccount.Balance += transferDetails.Amount;
        }

        private async Task RecordTransferTransactionAsync(TransferDto transferDetails)
        {
            // Gönderici için transaction kaydı oluştur.
            var fromTransaction = new Transaction
            {
                AccountId = transferDetails.FromAccountId,
                Amount = -transferDetails.Amount,
                TransactionDate = DateTime.UtcNow,
                Type = "transfer",
                Status = "completed"
            };

            // Alıcı için transaction kaydı oluştur.
            var toTransaction = new Transaction
            {
                AccountId = transferDetails.ToAccountId,
                Amount = transferDetails.Amount,
                TransactionDate = DateTime.UtcNow,
                Type = "transfer",
                Status = "completed"
            };

            await _context.Transactions.AddAsync(fromTransaction);
            await _context.Transactions.AddAsync(toTransaction);
            await _context.SaveChangesAsync();
        }


        private async Task LogErrorAsync(Exception ex)
        {
            string logMessage = $"Error: {ex.Message}, Stack Trace: {ex.StackTrace}\n";

            // Logları bir dosyaya asenkron olarak yaz
            string logFilePath = "error_log.txt";
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }


        // İşlem geçmişi sorgulama
        public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int accountId)
        {
            return await _context.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
        }
    }
}
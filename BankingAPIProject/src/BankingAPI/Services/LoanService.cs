
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly BankingContext _context;

        public LoanService(BankingContext context)
        {
            _context = context;
        }

        public async Task<Loan> ApplyForLoanAsync(LoanApplicationDto applicationDto)
        {
            // Kredi başvurusu işlemleri
            var loan = new Loan
            {
                UserId = applicationDto.UserId,
                Amount = applicationDto.Amount,
                InterestRate = applicationDto.InterestRate,
                DurationMonths = applicationDto.DurationMonths,
                StartDate = DateTime.UtcNow,
                Status = "Pending",
            };

            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();

            return loan;
        }



        public async Task<bool> MakePaymentAsync(PaymentDto paymentDto)
        {
            // Kredi ödeme işlemleri
            var loan = await _context.Loans.FindAsync(paymentDto.LoanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }

            // Ödeme işlemleri ve bakiye güncellemeleri...
            await _context.SaveChangesAsync();
            return true;
        }

        // Kredi detaylarını alma
        public async Task<Loan> GetLoanDetailsAsync(int loanId)
        {
            return await _context.Loans.FindAsync(loanId);
        }

        public async Task<LoanStatusDto> GetLoanStatusAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }

            return new LoanStatusDto
            {
                LoanId = loan.LoanId,
                Status = loan.Status
            };
        }

            public async Task<IEnumerable<Loan>> GetUserLoansAsync(int userId)
            {
                // Kullanıcının kredilerini alma işlemi
                return await _context.Loans.Where(l => l.UserId == userId).ToListAsync();
            }

        }
    }
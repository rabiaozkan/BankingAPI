using BankingAPI.Models;
using BankingAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly BankingContext _context;

        public LoanRepository(BankingContext context)
        {
            _context = context;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(int loanId)
        {
            return await _context.Loans.FindAsync(loanId);
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
        {
            return await _context.Loans
                                 .Where(l => l.UserId == userId)
                                 .ToListAsync();
        }

    }
}

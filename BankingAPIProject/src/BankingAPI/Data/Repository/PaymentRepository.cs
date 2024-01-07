using BankingAPI.Models;
using BankingAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BankingContext _context;

        public PaymentRepository(BankingContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAccountIdAsync(int accountId)
        {
            return await _context.Payments
                                 .Where(p => p.AccountId == accountId)
                                 .ToListAsync();
        }

    }
}

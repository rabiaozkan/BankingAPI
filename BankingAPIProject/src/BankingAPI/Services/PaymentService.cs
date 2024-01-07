using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAPI.Data;
using BankingAPI.DTOs;
using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BankingContext _context;

        public PaymentService(BankingContext context)
        {
            _context = context;
        }

        public async Task<Payment> SetupAutoPaymentAsync(AutoPaymentSetupDto setupDto)
        {
            // Otomatik ödeme ayarlarını yapma işlemleri
            var autoPayment = new Payment
            {
                UserId = setupDto.UserId,
                AccountId = setupDto.AccountId,
                Amount = setupDto.Amount,
                PaymentDate = setupDto.PaymentDate,
                PaymentType = "automatic",
                Status = "pending",
            };

            await _context.Payments.AddAsync(autoPayment);
            await _context.SaveChangesAsync();

            return autoPayment;
        }

        public async Task<bool> MakePaymentAsync(PaymentDto paymentDto)
        {
            // Manuel ödeme
            var payment = new Payment
            {
                UserId = paymentDto.UserId,
                AccountId = paymentDto.AccountId,
                Amount = paymentDto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentType = "manual",
                Status = "completed",
            };

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Payment>> GetPendingAutoPaymentsAsync()
        {
            return await _context.Payments
                .Where(p => p.Status == "pending" && p.PaymentType == "automatic")
                .ToListAsync();
        }

        public async Task<bool> ProcessPaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            // Ödeme işlemi burada gerçekleştirilecek
            // ödeme durumunu güncelle
            payment.Status = "completed";
            _context.Update(payment);
            await _context.SaveChangesAsync();

            return true;
        }
        // Ödeme geçmişi sorgulama
        public async Task<IEnumerable<Payment>> GetPaymentHistoryAsync(int accountId)
        {
            return await _context.Payments
                .Where(p => p.AccountId == accountId)
                .ToListAsync();
        }
    }
}
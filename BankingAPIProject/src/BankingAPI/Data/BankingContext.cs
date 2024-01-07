using Microsoft.EntityFrameworkCore;
using BankingAPI.Models;

namespace BankingAPI.Data
{
    public class BankingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User tablosunun konfigürasyonu
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            // Account tablosunun konfigürasyonu
            modelBuilder.Entity<Account>()
                .ToTable("Accounts")
                .HasKey(a => a.AccountId);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .IsRequired();

            // Transaction tablosunun konfigürasyonu
            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions")
                .HasKey(t => t.TransactionId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .IsRequired();

            // Loan tablosunun konfigürasyonu
            modelBuilder.Entity<Loan>()
                .ToTable("Loans")
                .HasKey(l => l.LoanId);

            modelBuilder.Entity<Loan>()
                .Property(l => l.Amount)
                .IsRequired();

            // Payment tablosunun konfigürasyonu
            modelBuilder.Entity<Payment>()
                .ToTable("Payments")
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .IsRequired();
        }
    }
}


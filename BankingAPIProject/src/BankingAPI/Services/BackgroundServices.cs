using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using BankingAPI.Services;

namespace BankingAPI.Services
{
    public class AutoPaymentBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AutoPaymentBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                    // Otomatik ödemeleri işleme
                    await ProcessAutoPayments(paymentService);

                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
            }
        }

        private async Task ProcessAutoPayments(IPaymentService paymentService)
        {
            var payments = await paymentService.GetPendingAutoPaymentsAsync();

            foreach (var payment in payments)
            {
                try
                {
                    await paymentService.ProcessPaymentAsync(payment.PaymentId);
                }
                catch (Exception ex)
                {
                    // Hata yönetimi
                    Console.WriteLine(ex.Message);
                    // Hata loglaması veya yönetimi için ek işlemler yapılabilir.
                }
            }
        }
    }
}

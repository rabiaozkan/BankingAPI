using Microsoft.AspNetCore.Mvc;
using BankingAPI.Services;
using BankingAPI.DTOs;
using BankingAPI.Models;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("setup")]
        public async Task<IActionResult> SetupAutoPayment([FromBody] AutoPaymentSetupDto setup)
        {
            try
            {
                var autoPayment = await _paymentService.SetupAutoPaymentAsync(setup);
                
                if (autoPayment != null)
                {
                    return Ok(new
                    {
                        message = "Auto payment setup successfully",
                        paymentId = autoPayment.PaymentId
                    });
                }

                return BadRequest(new
                {
                    message = "Auto payment setup failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("make")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto payment)
        {
            try
            {
                var paymentResult = await _paymentService.MakePaymentAsync(payment);

                if (paymentResult)
                {
                    return Ok(new
                    {
                        message = "Payment made successfully",
                        paymentId = payment.PaymentId // Ödeme ID'sini döndür
                    });
                }

                return BadRequest(new
                {
                    message = "Payment failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


    }
}
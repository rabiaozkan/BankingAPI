using Microsoft.AspNetCore.Mvc;
using BankingAPI.Services;
using BankingAPI.DTOs;
using BankingAPI.Models;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLoan([FromBody] LoanApplicationDto application)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak kredi başvurusu işlemleri
                var loan = await _loanService.ApplyForLoanAsync(application);

                if (loan != null)
                {
                    return Ok(new
                    {
                        message = "Loan application submitted successfully",
                        loanId = loan.LoanId
                    });
                }

                return BadRequest(new
                {
                    message = "Loan application failed"
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

        [HttpGet("status/{loanId}")]
        public IActionResult GetLoanStatus(int loanId)
        {
            try
            {
                var loanStatus = _loanService.GetLoanStatusAsync(loanId);

                if (loanStatus != null)
                {
                    return Ok(new
                    {
                        loanId = loanId,
                        status = loanStatus
                    });
                }

                return NotFound(new
                {
                    message = "Loan not found"
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

        [HttpPost("payment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto payment)
        {
            try
            {
                var result = await _loanService.MakePaymentAsync(payment);

                if (result)
                {
                    return Ok(new
                    {
                        message = "Payment made successfully"
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
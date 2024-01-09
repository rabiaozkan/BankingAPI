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

        /// <summary>
        /// Applies for a loan.
        /// </summary>
        /// <param name="application">Loan application details.</param>
        /// <returns>Result of the loan application.</returns>
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForLoan([FromBody] LoanApplicationDto application)
        {
            var loan = await _loanService.ApplyForLoanAsync(application);

            if (loan != null)
            {
                return Ok(new
                {
                    message = "Loan application submitted successfully",
                    loanId = loan.LoanId
                });
            }

            return BadRequest(new { message = "Loan application failed" });
        }

        /// <summary>
        /// Gets the status of a loan.
        /// </summary>
        /// <param name="loanId">The ID of the loan.</param>
        /// <returns>Status of the loan.</returns>
        [HttpGet("status/{loanId}")]
        public async Task<IActionResult> GetLoanStatus(int loanId)
        {
            var loanStatus = await _loanService.GetLoanStatusAsync(loanId);

            if (loanStatus != null)
            {
                return Ok(new
                {
                    loanId = loanId,
                    status = loanStatus
                });
            }

            return NotFound(new { message = "Loan not found" });
        }

        /// <summary>
        /// Makes a payment on a loan.
        /// </summary>
        /// <param name="payment">Payment details.</param>
        /// <returns>Result of the payment transaction.</returns>
        [HttpPost("payment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto payment)
        {
            var result = await _loanService.MakePaymentAsync(payment);

            if (result)
            {
                return Ok(new { message = "Payment made successfully" });
            }

            return BadRequest(new { message = "Payment failed" });
        }
    }
}

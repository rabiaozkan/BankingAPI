using Microsoft.AspNetCore.Mvc;
using BankingAPI.DTOs;
using BankingAPI.Services;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Deposits money into an account.
        /// </summary>
        /// <param name="transaction">Transaction details for the deposit.</param>
        /// <returns>Response for the deposit operation.</returns>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto transaction)
        {
            var depositResult = await _transactionService.DepositAsync(transaction);

            if (depositResult)
            {
                return Ok(new { message = "Deposit successful" });
            }

            return BadRequest(new { message = "Deposit failed" });
        }

        /// <summary>
        /// Withdraws money from an account.
        /// </summary>
        /// <param name="transaction">Transaction details for the withdrawal.</param>
        /// <returns>Response for the withdrawal operation.</returns>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto transaction)
        {
            var withdrawResult = await _transactionService.WithdrawAsync(transaction);

            if (withdrawResult)
            {
                return Ok(new { message = "Withdrawal successful" });
            }

            return BadRequest(new { message = "Withdrawal failed" });
        }

        /// <summary>
        /// Transfers money between accounts.
        /// </summary>
        /// <param name="transfer">Transfer details.</param>
        /// <returns>Response for the transfer operation.</returns>
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transfer)
        {
            var transferResult = await _transactionService.TransferAsync(transfer);

            if (transferResult)
            {
                return Ok(new { message = "Transfer successful" });
            }

            return BadRequest(new { message = "Transfer failed" });
        }
    }
}

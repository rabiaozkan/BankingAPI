using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BankingAPI.Services;
using BankingAPI.DTOs;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="accountCreation">Account creation details.</param>
        /// <returns>Response for account creation.</returns>
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationDto accountCreation)
        {
            var account = await _accountService.CreateAccountAsync(accountCreation);

            if (account != null)
            {
                return Ok(new
                {
                    message = "Account created successfully",
                    accountId = account.AccountId
                });
            }

            return BadRequest(new { message = "Account creation failed" });
        }

        /// <summary>
        /// Gets the balance of a specific account.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <returns>Account balance information.</returns>
        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _accountService.GetBalanceAsync(accountId);

            if (balance != null)
            {
                return Ok(new
                {
                    accountId = accountId,
                    balance = balance
                });
            }

            return NotFound(new { message = "Account not found" });
        }

        /// <summary>
        /// Updates the balance of a specific account.
        /// </summary>
        /// <param name="accountId">The account ID to be updated.</param>
        /// <param name="balanceUpdate">Balance update details.</param>
        /// <returns>Response for balance update.</returns>
        [HttpPut("update/{accountId}")]
        [Authorize(Roles = "admin,manager")]
        public async Task<IActionResult> UpdateBalance(int accountId, [FromBody] BalanceUpdateDto balanceUpdate)
        {
            var result = await _accountService.UpdateBalanceAsync(accountId, balanceUpdate);

            if (result)
            {
                return Ok(new { message = "Balance updated successfully" });
            }

            return BadRequest(new { message = "Balance update failed" });
        }
    }
}

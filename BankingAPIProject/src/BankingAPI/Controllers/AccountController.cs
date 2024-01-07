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

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreationDto accountCreation)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak yeni hesap oluşturma işlemleri
                var account = await _accountService.CreateAccountAsync(accountCreation);

                if (account != null)
                {
                    return Ok(new
                    {
                        message = "Account created successfully",
                        accountId = account.AccountId
                    });
                }

                return BadRequest(new
                {
                    message = "Account creation failed"
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

        [HttpGet("balance/{accountId}")]
        public IActionResult GetBalance(int accountId)
        {
            try
            {
                var balance = _accountService.GetBalanceAsync(accountId);

                if (balance != null)
                {
                    return Ok(new
                    {
                        accountId = accountId,
                        balance = balance
                    });
                }

                return NotFound(new
                {
                    message = "Account not found"
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

        [HttpPut("update/{accountId}")]
        [Authorize(Roles = "admin,manager")] // Rol tabanlı erişim kontrolü eklendi
        public async Task<IActionResult> UpdateBalance(int accountId, [FromBody] BalanceUpdateDto balanceUpdate)
        {
            try
            {
                var result = await _accountService.UpdateBalanceAsync(accountId, balanceUpdate);

                if (result)
                {
                    return Ok(new { message = "Balance updated successfully" });
                }

                return BadRequest(new { message = "Balance update failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
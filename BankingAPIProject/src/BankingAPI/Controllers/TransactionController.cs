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

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto transaction)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak para yatırma işlemleri
                var depositResult = await _transactionService.DepositAsync(transaction);

                if (depositResult)
                {
                    return Ok(new
                    {
                        message = "Deposit successful"
                    });
                }

                return BadRequest(new
                {
                    message = "Deposit failed"
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

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto transaction)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak para çekme işlemleri
                var withdrawResult = await _transactionService.WithdrawAsync(transaction);

                if (withdrawResult)
                {
                    return Ok(new
                    {
                        message = "Withdrawal successful"
                    });
                }

                return BadRequest(new
                {
                    message = "Withdrawal failed"
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

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transfer)
        {
            try
            {
                // DTO'dan gelen verileri kullanarak para transferi işlemleri
                var transferResult = await _transactionService.TransferAsync(transfer);

                if (transferResult)
                {
                    return Ok(new
                    {
                        message = "Transfer successful"
                    });
                }

                return BadRequest(new
                {
                    message = "Transfer failed"
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
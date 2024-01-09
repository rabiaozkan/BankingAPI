using Microsoft.AspNetCore.Mvc;
using BankingAPI.DTOs;
using BankingAPI.Services;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        /// <summary>
        /// Creates a support request.
        /// </summary>
        /// <param name="supportRequest">Support request details.</param>
        /// <returns>Response for support request creation.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateSupportRequest([FromBody] SupportRequestDto supportRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRequest = await _supportService.CreateSupportRequestAsync(supportRequest);

            return CreatedAtAction(nameof(GetRequestStatus), new { requestId = createdRequest.Id }, createdRequest);
        }

        /// <summary>
        /// Gets the status of a support request.
        /// </summary>
        /// <param name="requestId">The ID of the support request.</param>
        /// <returns>Status of the support request.</returns>
        [HttpGet("status/{requestId}")]
        public async Task<IActionResult> GetRequestStatus(int requestId)
        {
            var requestStatus = await _supportService.GetRequestStatusAsync(requestId);

            if (requestStatus == null)
            {
                return NotFound(new { message = "Request not found" });
            }

            return Ok(requestStatus);
        }
    }
}

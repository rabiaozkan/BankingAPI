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
            // SupportService enjekte ediliyor
            _supportService = supportService;
        }

        [HttpPost("create")]
        public IActionResult CreateSupportRequest([FromBody] SupportRequestDto supportRequest)
        {
            // Kullanıcıdan gelen destek talebi DTO'sunu alıyoruz.

            // DTO'nun geçerli olduğunu doğrulayın
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Destek talebini işlemek için SupportService kullanılıyor
                var createdRequest = _supportService.CreateSupportRequestAsync(supportRequest);

                // Başarılı bir şekilde oluşturulmuşsa, oluşturulan talebin detaylarını dön
                return CreatedAtAction(nameof(GetRequestStatus), new { requestId = createdRequest.Id }, createdRequest);
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata oluşursa, hata mesajını dön
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("status/{requestId}")]
        public IActionResult GetRequestStatus(int requestId)
        {
            // Belirli bir destek talebinin durumunu sorgulama

            try
            {
                // Talebin durumunu al
                var requestStatus = _supportService.GetRequestStatusAsync(requestId);

                // Talep bulunamazsa, NotFound döndür
                if (requestStatus == null)
                {
                    return NotFound(new { message = "Request not found" });
                }

                // Talebin durumunu dön
                return Ok(requestStatus);
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata oluşursa, hata mesajını dön
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

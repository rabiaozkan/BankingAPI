using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Data;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public class SupportService : ISupportService
    {
        private readonly BankingContext _context;

        public SupportService(BankingContext context)
        {
            _context = context;
        }

        public async Task<SupportRequest> CreateSupportRequestAsync(SupportRequestDto supportRequestDto)
        {
            var supportRequest = new SupportRequest
            {
                Description = supportRequestDto.Description,
                UserId = supportRequestDto.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = "Open"
            };

            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync();

            return supportRequest;
        }

        public async Task<SupportRequest> GetRequestStatusAsync(int requestId)
        {
            return await Task.Run(() => _context.SupportRequests.Find(requestId));
        }
    }
}

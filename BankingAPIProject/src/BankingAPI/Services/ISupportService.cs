using BankingAPI.Models;
using BankingAPI.DTOs;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface ISupportService
    {
        Task<SupportRequest> CreateSupportRequestAsync(SupportRequestDto supportRequestDto);
        Task<SupportRequest> GetRequestStatusAsync(int requestId);
    }
}

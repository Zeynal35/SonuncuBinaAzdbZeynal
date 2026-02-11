using Domain.Entities;

namespace Persistence.Services
{
    public interface IRefreshTokenService
    {
        Task<string> CreateAsync(User user);
        Task<string> CreateAsync(User user);
        Task<User?> ValidateAndConsumeAsync(string token);
        Task<User?> ValidateAndConsumeAsync(string token);
    }
}
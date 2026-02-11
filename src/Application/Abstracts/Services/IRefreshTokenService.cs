namespace Application.Abstracts.Services;
using Domain.Entities;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(User user);
    Task<User?> ValidateAndConsumeAsync(string token);
}

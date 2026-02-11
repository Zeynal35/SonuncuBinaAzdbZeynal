using Domain.Entities;

namespace Application.Abstracts.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenWithUserAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task DeleteByTokenAsync(string token);
}

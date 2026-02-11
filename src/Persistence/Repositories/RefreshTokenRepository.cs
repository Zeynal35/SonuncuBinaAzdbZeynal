using Application.Abstracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly BinaLiteDbContext _context;

    public RefreshTokenRepository(BinaLiteDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenWithUserAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByTokenAsync(string token)
    {
        await _context.RefreshTokens
            .Where(x => x.Token == token)
            .ExecuteDeleteAsync();
    }
}

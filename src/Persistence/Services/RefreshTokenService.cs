using System.Security.Cryptography;
using System.Text;
using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Options;
using Domain.Entities;
using Microsoft.Extensions.Options;

namespace Persistence.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenService(
        IRefreshTokenRepository repository,
        IOptions<JwtOptions> jwtOptions)
    {
        _repository = repository;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> CreateAsync(User user)
    {
        // 32 byte random token
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        var token = Convert.ToHexString(randomBytes);

        var refreshToken = new RefreshToken
        {
            Token = token,
            UserId = user.Id,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow
                .AddMinutes(_jwtOptions.RefreshExpirationMinutes)
        };

        await _repository.AddAsync(refreshToken);

        return token;
    }

    public async Task<User?> ValidateAndConsumeAsync(string token)
    {
        var refreshToken = await _repository.GetByTokenWithUserAsync(token);

        if (refreshToken == null)
            return null;

        if (refreshToken.ExpiresAtUtc <= DateTime.UtcNow)
            return null;

        // bir dəfə istifadə üçün silirik
        await _repository.DeleteByTokenAsync(token);

        return refreshToken.User;
    }
}



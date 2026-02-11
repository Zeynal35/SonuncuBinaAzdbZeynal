using Application.DTOs.Auth;

namespace Application.Abstracts.Services;

public interface IAuthService
{
    Task<(bool Success, string? Error)> RegisterAsync(
        RegisterReq request,
        CancellationToken ct = default);

    // 🔥 dəyişdi
    Task<TokenResponse?> LoginAsync(
        LoginReq request,
        CancellationToken ct = default);

    // 🔥 yeni əlavə
    Task<TokenResponse?> RefreshTokenAsync(
        string refreshToken,
        CancellationToken ct = default);
}

using Application.Abstracts.Services;
using Application.DTOs.Auth;
using Application.Options;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IOptions<JwtOptions> jwtOptions;



    public AuthService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenService refreshTokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenService = refreshTokenService;
        _jwtOptions = jwtOptions.Value;
    }

   

    public async Task<(bool Success, string? Error)> RegisterAsync(
        RegisterReq request,
        CancellationToken ct = default)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }

        return (true, null);
    }

    public async Task<TokenResponse?> LoginAsync(
        LoginReq request,
        CancellationToken ct = default)
    {
        User? user =
            await _userManager.FindByEmailAsync(request.Login)
            ?? await _userManager.FindByNameAsync(request.Login);

        if (user is null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            return null;

        return await BuildTokenResponseAsync(user);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(
        string refreshToken,
        CancellationToken ct = default)
    {
        var user = await _refreshTokenService.ValidateAndConsumeAsync(refreshToken);

        if (user is null)
            return null;

        return await BuildTokenResponseAsync(user);
    }

    private async Task<TokenResponse> BuildTokenResponseAsync(User user)
    {
        var accessToken = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = await _refreshTokenService.CreateAsync(user);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = DateTime.UtcNow
                .AddMinutes(_jwtOptions.ExpirationMinutes)
        };
    }
}

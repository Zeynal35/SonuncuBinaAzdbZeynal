using Application.Abstracts.Services;
using Application.DTOs.Auth;
using Application.Options;
using Domain.Constants;
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
    private readonly JwtOptions _jwtOptions;

    public AuthService(
        UserManager<User> userManager,
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

    public async Task<(bool Success, string? Error)> RegisterAsync(RegisterReq request, CancellationToken ct = default)
    {
        var exists = await _userManager.FindByEmailAsync(request.Email);
        if (exists is not null)
            return (false, "Bu email artıq qeydiyyatdan keçib.");

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.UserName // sən RegisterReq-də UserName istifadə edirdin
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var err = string.Join(" | ", result.Errors.Select(e => e.Description));
            return (false, err);
        }

        // default rol
        await _userManager.AddToRoleAsync(user, RoleNames.User);

        return (true, null);
    }

    public async Task<string?> LoginAsync(LoginReq request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(request.Login)
                   ?? await _userManager.FindByEmailAsync(request.Login);

        if (user is null)
            return null;

        var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!signIn.Succeeded)
            return null;

        var tokenResponse = await BuildTokenResponseAsync(user);

        // səndə LoginAsync string qaytarırdı — AccessToken qaytarıram
        return tokenResponse.AccessToken;
    }

    private async Task<TokenResponse> BuildTokenResponseAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user, roles);

        var refreshToken = await _refreshTokenService.CreateAsync(user);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes)
        };
    }
}


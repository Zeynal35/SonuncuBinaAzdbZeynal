// 3️⃣ FAYL: src/API/Controllers/AuthController.cs
using Application.Abstracts.Services;
using Application.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterReq request)
    {
        var (success, error) = await _authService.RegisterAsync(request);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(new { message = "Qeydiyyat uğurla tamamlandı. İndi login ola bilərsiniz." });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginReq request)
    {
        _logger.LogInformation("🔑 Login attempt for: {Login}", request.Login);

        var result = await _authService.LoginAsync(request);

        if (result is null)
        {
            _logger.LogWarning("   ❌ Login failed for: {Login}", request.Login);
            return Unauthorized("Login və ya şifrə yanlışdır");
        }

        _logger.LogInformation("   ✅ Login successful!");
        _logger.LogInformation("   Token issued (first 50 chars): {Token}...",
            result.AccessToken[..Math.Min(50, result.AccessToken.Length)]);

        return Ok(result);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest("Refresh token boş ola bilməz");

        var result = await _authService
            .RefreshTokenAsync(request.RefreshToken);

        if (result is null)
            return Unauthorized("Refresh token yanlışdır və ya vaxtı keçib");

        return Ok(result);
    }
}


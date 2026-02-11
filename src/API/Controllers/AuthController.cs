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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginReq request)
    {
        var result = await _authService.LoginAsync(request);

        if (result is null)
            return Unauthorized("Login və ya şifrə yanlışdır");

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


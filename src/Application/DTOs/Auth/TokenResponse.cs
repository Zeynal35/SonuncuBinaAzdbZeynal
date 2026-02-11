namespace Application.DTOs.Auth;

public class TokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAtUtc { get; set; }
}

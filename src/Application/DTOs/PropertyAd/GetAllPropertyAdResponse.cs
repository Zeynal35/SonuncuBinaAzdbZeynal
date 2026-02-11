namespace Application.DTOs.PropertyAd;

public class GetAllPropertyAdResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    // ƏLAVƏ (profile xətası qalxsın deyə)
    public string? FirstMediaKey { get; set; }
}

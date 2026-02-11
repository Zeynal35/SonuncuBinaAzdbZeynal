namespace Application.DTOs.Street;

public class UpdateStreetRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

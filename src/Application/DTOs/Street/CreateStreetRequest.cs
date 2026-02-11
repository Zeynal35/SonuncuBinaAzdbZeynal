namespace Application.DTOs.Street;

public class CreateStreetRequest
{
    public string Name { get; set; } = null!;
    public double Length { get; set; }
    public string? Description { get; set; }
    public int CityId { get; set; }
}


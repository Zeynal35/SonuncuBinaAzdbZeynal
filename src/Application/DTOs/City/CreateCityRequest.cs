namespace Application.DTOs.City;

public class CreateCityRequest
{
    public string Name { get; set; } = null!;
    public double Area { get; set; }
    public int Population { get; set; }
    public bool IsCapital { get; set; }
    public bool IsTouristic { get; set; }
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? FlagUrl { get; set; }
    public int Elevation { get; set; }
    public decimal GDP { get; set; }
    public double Density { get; set; }
    public string? TransportSystem { get; set; }
    public int AirportsCount { get; set; }
    public int UniversitiesCount { get; set; }
    public int HospitalsCount { get; set; }
}

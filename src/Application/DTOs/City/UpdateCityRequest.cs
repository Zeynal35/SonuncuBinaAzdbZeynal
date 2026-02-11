namespace Application.DTOs.City;

public class UpdateCityRequest
{
    public string Name { get; set; } = null!;
    public double Area { get; set; }
    public int Population { get; set; }
    public bool IsCapital { get; set; }

    public int AirportsCount { get; set; }
    public int UniversitiesCount { get; set; }
    public int HospitalsCount { get; set; }

}

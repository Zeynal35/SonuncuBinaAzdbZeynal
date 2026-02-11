namespace Application.DTOs.City;

public class GetAllCityResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Area { get; set; }
    public int Population { get; set; }

}

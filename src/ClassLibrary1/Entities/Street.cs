namespace Domain.Entities;

public class Street: BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public double Length { get; set; }
    public string? Description { get; set; }
    public int CityId { get; set; }
    public City City { get; set; } = null!;
}

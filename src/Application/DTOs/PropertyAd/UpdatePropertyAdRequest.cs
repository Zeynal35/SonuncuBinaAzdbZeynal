using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PropertyAd;

public class UpdatePropertyAdRequest
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }


}

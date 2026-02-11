using Microsoft.AspNetCore.Http;

namespace Application.Dtos.PropertyAd;

public class CreatePropertyMediaDto
{
    public int Id { get; set; } 
    public string ObjectKey { get; set; }
    public int PropertyAdId { get; set; }
    public int Order { get; set; }
    public IFormFile File { get; set; } = default!;
}

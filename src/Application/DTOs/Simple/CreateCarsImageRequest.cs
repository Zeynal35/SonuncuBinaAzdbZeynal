using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Simple;

public class CreateCarsImageRequest
{
    public IFormFile File { get; set; } = null!;

}

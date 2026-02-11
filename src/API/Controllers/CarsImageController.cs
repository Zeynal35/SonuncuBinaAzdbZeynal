using Application.Abstracts.Services.Simple;
using Application.DTOs.Simple;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsImageController : ControllerBase
{
    private readonly ICarsImageService _service;

    public CarsImageController(ICarsImageService service)
    {
        _service = service;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] CreateCarsImageRequest request)
    {
        await _service.CreateAsync(request);
        return Ok("Image uploaded successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
    [HttpPost("test")]
    public IActionResult Test(IFormFile file)
    {
        return Ok(file.FileName);
    }
}

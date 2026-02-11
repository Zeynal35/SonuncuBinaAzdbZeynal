using Application.Abstracts.Services;
using Application.Dtos.PropertyAd;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyMediaController : ControllerBase
{
    private readonly IPropertyMediaService _service;

    public PropertyMediaController(IPropertyMediaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Property üçün şəkil əlavə edir
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreatePropertyMediaDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("File boş ola bilməz");

        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }
}


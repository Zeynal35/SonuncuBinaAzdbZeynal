// 2️⃣ FAYL: src/API/Controllers/CityController.cs
using Application.Abstracts.Services;
using Application.DTOs.City;
using Application.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices _cityServices;
        private readonly ILogger<CityController> _logger;

        public CityController(
            ICityServices cityServices,
            ILogger<CityController> logger)
        {
            _cityServices = cityServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cities = await _cityServices.GetAllAsync();
            if (cities == null || !cities.Any())
                return NotFound("No cities found.");

            return Ok(cities);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BaseResponse>> CreateAsync([FromBody] CreateCityRequest request)
        {
            _logger.LogInformation("🏙️ City POST endpoint hit!");
            _logger.LogInformation("   User.Identity.IsAuthenticated: {IsAuth}", User.Identity?.IsAuthenticated ?? false);
            _logger.LogInformation("   User.Identity.Name: {Name}", User.Identity?.Name ?? "NULL");

            if (User.Claims.Any())
            {
                _logger.LogInformation("   User Claims:");
                foreach (var claim in User.Claims)
                {
                    _logger.LogInformation("      {Type} = {Value}", claim.Type, claim.Value);
                }
            }
            else
            {
                _logger.LogWarning("   ⚠️ NO CLAIMS FOUND! User is NOT authenticated!");
            }

            var authHeader = Request.Headers["Authorization"].ToString();
            _logger.LogInformation("   Authorization Header: {Header}",
                string.IsNullOrEmpty(authHeader) ? "EMPTY!" : authHeader[..Math.Min(50, authHeader.Length)] + "...");

            var ok = await _cityServices.CreateAsync(request);
            if (!ok)
                return BadRequest(BaseResponse.Fail("City could not be created."));

            return Ok(BaseResponse.Ok("Created"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var city = await _cityServices.GetByIdAsync(id);
            return Ok(city);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> UpdateAsync(int id, [FromBody] UpdateCityRequest request)
        {
            var ok = await _cityServices.UpdateAsync(id, request);
            if (!ok)
                return BadRequest(BaseResponse.Fail("City could not be updated."));

            return Ok(BaseResponse.Ok("Updated"));
        }
    }
}

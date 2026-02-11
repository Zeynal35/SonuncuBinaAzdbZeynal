using Application.Abstracts.Services;
using Application.DTOs.City;
using Application.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServices _cityServices;
        public CityController(ICityServices cityServices)
        {
            _cityServices = cityServices;
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
        public async Task<ActionResult<BaseResponse>> CreateAsync([FromBody] CreateCityRequest request)
        {
            var ok = await _cityServices.CreateAsync(request);
            if (!ok) return BadRequest(BaseResponse.Fail("City could not be created."));
            return Ok(BaseResponse.Ok("Created"));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var city = await _cityServices.GetByIdAsync(id);
            return Ok(city);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<BaseResponse>> UpdateAsync(int id, [FromBody] UpdateCityRequest request)
        {
            var ok = await _cityServices.UpdateAsync(id, request);
            if (!ok) return BadRequest(BaseResponse.Fail("City could not be updated."));
            return Ok(BaseResponse.Ok("Updated"));
        }
    }
}

using Application.Abstracts.Services;
using Application.DTOs.Street;
using Application.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly IStreetService _streetService;
        public StreetController(IStreetService streetService)
        {
            _streetService = streetService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var streets = await _streetService.GetAllAsync();
            if (streets == null || !streets.Any())
                return NotFound("No streets found.");
            return Ok(streets);
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateAsync([FromBody] CreateStreetRequest request)
        {
            var ok = await _streetService.CreateAsync(request);
            if (!ok) return BadRequest(BaseResponse.Fail("Street could not be created."));
            return Ok(BaseResponse.Ok("Created"));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var street = await _streetService.GetByIdAsync(id);
            return Ok(street);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateStreetRequest request)
        {
            var ok = await _streetService.UpdateAsync(id, request);
            if (!ok) return BadRequest(BaseResponse.Fail("Street could not be updated."));
            return Ok(BaseResponse.Ok("Updated"));
        }
    }
}

using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.PropertyAd;
using Application.Shared.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyAdController : ControllerBase
{
    private readonly IPropertyAdService _propertyAdService;
    public PropertyAdController(IPropertyAdService propertyAdService)
    {
        _propertyAdService = propertyAdService;
    }
    [HttpGet]
    public async  Task<IActionResult> GetAllAsync()
    {
        var propertyAds = await _propertyAdService.GetAllAsync();
        return Ok(propertyAds);
    }
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateAsync([FromBody] CreatePropertyAdRequest request)
    {
       var ok= await _propertyAdService.CreateAsync(request);
        if(!ok) return BadRequest(BaseResponse.Fail("Could not create PropertyAd"));

        return Ok(BaseResponse.Ok("Created."));
    }
    [HttpGet]
    [Route("{id}")]
    public async  Task<IActionResult> GetByIdAsync(int id)
    {
        var propertyAd = await _propertyAdService.GetByIdAsync(id);
        if (propertyAd == null)
            return NotFound();
        return Ok(propertyAd);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<BaseResponse>> UpdateAsync(int id, [FromBody] UpdatePropertyAdRequest request)
    {
        var ok = await _propertyAdService.UpdateAsync(id,request);
        if (!ok) return BadRequest(BaseResponse.Fail("Could not update PropertyAd"));
        return Ok(BaseResponse.Ok("Updated."));
    }



}

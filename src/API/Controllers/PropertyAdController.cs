using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAdController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Public GET: PropertyAd list");
    }

    // giriş etmiş user (Admin də daxil)
    [HttpPost]
    [Authorize(Policy = Policies.ManageProperties)]
    public IActionResult Create()
    {
        return Ok("Auth user: PropertyAd created");
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = Policies.ManageProperties)]
    public IActionResult Update(int id)
    {
        return Ok($"Auth user: PropertyAd {id} updated");
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.ManageProperties)]
    public IActionResult Delete(int id)
    {
        return Ok($"Auth user: PropertyAd {id} deleted");
    }
}

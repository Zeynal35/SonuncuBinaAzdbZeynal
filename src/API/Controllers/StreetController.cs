using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StreetController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Public GET: Street list");
    }

    [HttpPost]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Create()
    {
        return Ok("Admin only: Street created");
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Update(int id)
    {
        return Ok($"Admin only: Street {id} updated");
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Delete(int id)
    {
        return Ok($"Admin only: Street {id} deleted");
    }
}

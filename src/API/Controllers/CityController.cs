using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    // private readonly ICityService _service;
    // public CityController(ICityService service) => _service = service;

    // GET-lər çox vaxt açıq qalır (Authorize qoymaya da bilərsən)
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("Public GET: City list");
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        return Ok($"Public GET: City {id}");
    }

    // yalnız Admin
    [HttpPost]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Create()
    {
        return Ok("Admin only: City created");
    }

    // yalnız Admin
    [HttpPut("{id:int}")]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Update(int id)
    {
        return Ok($"Admin only: City {id} updated");
    }

    // yalnız Admin
    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.ManageCities)]
    public IActionResult Delete(int id)
    {
        return Ok($"Admin only: City {id} deleted");
    }
}

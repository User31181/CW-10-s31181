using Microsoft.AspNetCore.Mvc;
using Zad12.Services;

namespace Zad12.Controllers;

[ApiController]
[Route("api")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _service;

    public TripsController(ITripsService service)
    {
        _service = service;
    }
    
    [HttpGet("trips")]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (trips, totalPages) = await _service.GetTripsAsync(page, pageSize);

        return Ok(new
        {
            pageNum = page,
            pageSize = pageSize,
            allPages = totalPages,
            trips = trips
        });
    }
}
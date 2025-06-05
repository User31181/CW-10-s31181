using Microsoft.AspNetCore.Mvc;
using Zad12.DTOs;
using Zad12.Services;

namespace Zad12.Controllers;

[ApiController]
[Route("api")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _service;

    public ClientsController(IClientsService service)
    {
        _service = service;
    }

    [HttpDelete("clients/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var (success, error) = await _service.DeleteClientAsync(idClient);

        if (!success)
        {
            if (error == "Client not found")
                return NotFound(error);
            return BadRequest(error);
        }

        return NoContent();
    }
    
    [HttpPost("/api/trips/{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripDto dto)
    {
        var (success, error) = await _service.AssignClientToTripAsync(idTrip, dto);

        if (!success)
            return BadRequest(error);

        return StatusCode(201); // Created
    }
}
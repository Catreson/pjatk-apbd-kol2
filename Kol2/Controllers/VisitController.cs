using Kol2.DTOs;
using Kol2.Exceptions;
using Kol2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kol2.Controllers;

[ApiController]
[Route("api/visits")]
public class CustomersController : ControllerBase
{
    private readonly IVisitService _service;
    public CustomersController(IVisitService service) => _service = service;

    [HttpGet("{id}/")]
    public async Task<IActionResult> GetVisit(int id)
    {
        try   { return Ok(await _service.GetVisitAsync(id)); }
        catch (NotFoundException e) { return NotFound(e.Message); }
    }

    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
    // {
    //     try
    //     {
    //         await _service.CreateAsync(dto);
    //         return Created();
    //     }
    //     catch (NotFoundException e)   { return NotFound(e.Message); }
    //     catch (ConflictException e)   { return Conflict(e.Message); }
    //     catch (BadRequestException e) { return BadRequest(e.Message); }
    // }
}
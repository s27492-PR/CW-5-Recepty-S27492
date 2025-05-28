using Microsoft.AspNetCore.Mvc;
using Recepty.DTOs;
using Recepty.Exceptions;
using Recepty.Services;

namespace Recepty.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDTO addPrescriptionDTO)
    {
        try
        {
            var prescriptionId = await _prescriptionService.AddPrescriptionAsync(addPrescriptionDTO);
            return CreatedAtAction(nameof(AddPrescription), new { id = prescriptionId }, new { id = prescriptionId });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd podczas dodawania recepty: {ex.Message}");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Recepty.DTOs;
using Recepty.Exceptions;
using Recepty.Services;

namespace Recepty.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        try
        {
            var patientDetails = await _patientService.GetPatientDetailsAsync(idPatient);
            return Ok(patientDetails);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd podczas pobierania danych pacjenta: {ex.Message}");
        }
    }
}
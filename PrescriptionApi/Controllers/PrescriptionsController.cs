using Microsoft.AspNetCore.Mvc;
using PrescriptionApi.DTOs;
using PrescriptionApi.Exceptions;
using PrescriptionApi.Services;

namespace PrescriptionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController (IDbService dbService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto dto)
    {
        try
        {
            var prescriptionId = await dbService.AddPrescriptionAsync(dto);
            return Ok(prescriptionId);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
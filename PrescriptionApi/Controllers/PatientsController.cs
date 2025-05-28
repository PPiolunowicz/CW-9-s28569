using Microsoft.AspNetCore.Mvc;
using PrescriptionApi.DTOs;
using PrescriptionApi.Exceptions;
using PrescriptionApi.Models;
using PrescriptionApi.Services;

namespace PrescriptionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController (IDbService dbService) : ControllerBase
{
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient([FromRoute] int id)
    {
        try
        {
            return Ok(await dbService.GetPatientDetailsAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }




}
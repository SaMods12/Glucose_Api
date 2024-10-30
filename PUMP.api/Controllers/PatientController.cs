using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.api.Controllers;

[ApiController]
[Route("v1/patient")]

public class PatientController : ControllerBase
{
    private readonly IPatient _patient;

    public PatientController(IPatient patient)
    {
        _patient = patient;
    }

    [Authorize(Policy = "General")]
    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
    {
        var result = await this._patient.Create(patient);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize(Policy = "General")]
    [HttpGet]
    public async Task<IActionResult> ReadMembers(int? id)
    {
        var result = await this._patient.Read(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Policy = "General")]
    [HttpPut]
    public async Task<IActionResult> UpdatePatient([FromBody] Patient patient)
    {
        var result = await this._patient.Update(patient);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize(Policy = "Administrator")]
    [HttpDelete]
    public async Task<IActionResult> DeletePatient(int? id)
    {
        var result = await this._patient.Delete(id);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

}
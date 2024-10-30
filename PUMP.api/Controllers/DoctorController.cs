using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.api.Controllers;

[ApiController]
[Route("v1/doctor")]
public class DoctorController : ControllerBase
{
    private readonly IDoctor _doctor;

    public DoctorController(IDoctor doctor)
    {
        _doctor = doctor;
    }

    [Authorize(Policy = "General")]
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
    {
        var result = await this._doctor.Create(doctor);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize(Policy = "General")]
    [HttpGet]
    public async Task<IActionResult> ReadDoctors(int? id)
    {
        var result = await this._doctor.Read(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Policy = "General")]
    [HttpPut]
    public async Task<IActionResult> UpdateDoctor([FromBody] Doctor doctor)
    {
        var result = await this._doctor.Update(doctor);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize(Policy = "Administrator")]
    [HttpDelete]
    public async Task<IActionResult> DeleteDoctor(int? id)
    {
        var result = await this._doctor.Delete(id);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.api.Controllers;

[ApiController]
[Route("v1/appointment")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointment _appointment;

    public AppointmentController(IAppointment appointment)
    {
        _appointment = appointment;
    }

    // [Authorize(Policy = "General")] //
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        var result = await this._appointment.Create(appointment);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // [Authorize(Policy = "General")] //
    [HttpGet]
    public async Task<IActionResult> ReadAppointment(int? id)
    {
        var result = await this._appointment.Read(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    //[Authorize(Policy = "General")]//
    [HttpPut]
    public async Task<IActionResult> UpdateAppointment([FromBody] Appointment appointment)
    {
        var result = await this._appointment.Update(appointment);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    //[Authorize(Policy = "Administrator")]//
    [HttpDelete]
    public async Task<IActionResult> DeleteAppointment(int? id)
    {
        var result = await this._appointment.Delete(id);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}

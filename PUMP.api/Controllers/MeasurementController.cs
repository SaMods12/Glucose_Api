using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUMP.core.BL.Interfaces;
using PUMP.models;

namespace PUMP.api.Controllers;

[ApiController]
[Route("v1/measurement")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurement _measurement;

    public MeasurementController(IMeasurement measurement)
    {
        _measurement = measurement;
    }

    // [Authorize(Policy = "General")] //
    [HttpPost]
    public async Task<IActionResult> CreateMeasurement([FromBody] Measurement measurement)
    {
        var result = await this._measurement.Create(measurement);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // [Authorize(Policy = "General")] //
    [HttpGet]
    public async Task<IActionResult> ReadMeasurement(int? id)
    {
        var result = await this._measurement.Read(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    //[Authorize(Policy = "General")]//
    [HttpPut]
    public async Task<IActionResult> UpdateMeasurement([FromBody] Measurement measurement)
    {
        var result = await this._measurement.Update(measurement);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    //[Authorize(Policy = "Administrator")]//
    [HttpDelete]
    public async Task<IActionResult> DeleteMeasurement(int? id)
    {
        var result = await this._measurement.Delete(id);
        if (!result)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}

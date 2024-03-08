using Microsoft.AspNetCore.Mvc;
using WateringSimulatorMvc.Models;
using WateringSimulatorMvc.Services;

namespace WateringSimulatorMvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoilMoistureSensorController : ControllerBase
{
    private readonly SoilMoistureService _getSoilMoistureService;

    public SoilMoistureSensorController(SoilMoistureService getSoilMoistureService)
    {
        _getSoilMoistureService = getSoilMoistureService;
    }

    [HttpGet("GetSoilMoisture")]
    public ActionResult<WateringViewModel> Get()
    {
        var result = _getSoilMoistureService.Get();
        return Ok(result);
    }

    [HttpPost("set-turn-on-status/{turnOn}/{userId}")]
    public ActionResult<WateringViewModel> SetStatus(bool turnOn, int userId)
    {
        _getSoilMoistureService.TurnOn(turnOn, userId);
        return Ok();
    }
}

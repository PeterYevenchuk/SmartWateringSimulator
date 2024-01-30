using Microsoft.AspNetCore.Mvc;

namespace WateringSimulator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoilMoistureSensorController : ControllerBase
    {
        [HttpGet("GetSoilMoisture")]
        public ActionResult<double> Get()
        {
            var random = new Random();
            double soilMoisture = random.NextDouble() * 100;
            string formattedSoilMoisture = soilMoisture.ToString("0.00");
            return Ok(formattedSoilMoisture);
        }
    }
}

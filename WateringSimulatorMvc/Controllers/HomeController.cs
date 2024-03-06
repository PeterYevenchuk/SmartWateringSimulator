using Microsoft.AspNetCore.Mvc;
using WateringSimulatorMvc.Services;

namespace WateringSimulatorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoilMoistureService _getSoilMoistureService;

        public HomeController(SoilMoistureService getSoilMoistureService)
        {
            _getSoilMoistureService = getSoilMoistureService;
        }
        public ActionResult Index()
        {
            var result = _getSoilMoistureService.Get();
            return View(result);
        }
    }
}

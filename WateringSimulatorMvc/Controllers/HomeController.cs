using Microsoft.AspNetCore.Mvc;

namespace WateringSimulatorMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

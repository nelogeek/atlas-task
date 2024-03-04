using Microsoft.AspNetCore.Mvc;

namespace atlas_task.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

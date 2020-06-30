using Microsoft.AspNetCore.Mvc;

namespace MyConsultation.Controllers
{
    public class СonsultationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
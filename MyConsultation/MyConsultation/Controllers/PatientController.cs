using Microsoft.AspNetCore.Mvc;

namespace MyConsultation.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using ConsultationService.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyConsultation.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public IActionResult Journal()
        {
           var patiens = _patientService.GetAllAsync();
            return View(patiens);
        }

        [HttpPost]
        public JsonResult CheckSnilsValid(string dirtySnils)
        {
           bool isValid =  _patientService.CheckSnilsValid(dirtySnils);
            return Json(new { isValid });
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            _patientService.Save(patient);
            ViewBag.Information = "Пациент успешно сохранен!";
            return View("New", ViewBag);
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
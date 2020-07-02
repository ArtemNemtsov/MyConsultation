using ConsultationService.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MyConsultation.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> JournalAsync()
        {
           var patiens = _patientService.GetAll();
            return View(await patiens.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearshBySnils(string snils)
        {
            var patiens = _patientService.FindBySnils(snils);
            return View("Journal", await patiens.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearshByFIO(string FIO)
        {
            var patiens = _patientService.FindByFIO(FIO);
            return View("Journal", await patiens.ToListAsync());
        }

        [HttpPost]
        public JsonResult CheckSnilsValid(string newSnils)
        {
            var isNewSnils = _patientService.SnilsExist(newSnils);
            var isValid = isNewSnils && _patientService.CheckSnilsValid(newSnils); 
            return Json(new { isValid});
        }

        [HttpPost]
        public IActionResult Save(Patient patient)
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
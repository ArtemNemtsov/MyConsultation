using ConsultationService.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediaStudioService.Core.Classes;
using System;

namespace MyConsultation.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Journal()
        {
           var patiens = _patientService.GetAll();
           return View(await patiens.ToListAsync());
        }
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            _patientService.Create(patient);
            ViewBag.Information = "Пациент успешно сохранен!";
            return View("New", ViewBag);
        }

        public IActionResult Edit(int idPatient)
        {
            var patient = _patientService.Get(idPatient);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult Update(Patient patient)
        {
            if (!_patientService.PatientExist(patient.IdPatient))
            {
                return NotFound();
            }

            _patientService.Update(patient);
            ViewBag.Information = "Данные пациента успешно изменены!";
            return View("Edit", patient);
        }

        public IActionResult Delete(int idPatient)
        {
            if (!_patientService.PatientExist(idPatient))
            {
                return NotFound();
            }

            _patientService.Delete(idPatient);
            return RedirectToAction("Journal");
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
        public ActionResult<Responce> CheckSnils(string Snils, int? idPatient = null)
        {
            return SafeExecutor.Run(() => _patientService.CheckSnils(Snils, idPatient));
        }
    }
}
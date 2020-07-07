using ConsultationService.Services;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyConsultation.Controllers
{
    public class СonsultationController : Controller
    {
        private readonly СonsultationService _сonsultationService;

        public СonsultationController(СonsultationService сonsultationService)
        {
            _сonsultationService = сonsultationService;
        }

        [HttpGet]
        public async Task<IActionResult> Journal()
        {
            var consultation = _сonsultationService.GetAll();
            return View(await consultation.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            await FillViewBagPatientAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Сonsultation сonsultation)
        {
            _сonsultationService.Create(сonsultation);
            await FillViewBagPatientAsync();
            ViewBag.Information = "Консультация сохранена!";

            return View("Update", сonsultation);
        }

        [HttpGet]
        public async Task<IActionResult> Update(long idConsultation)
        {
            if (!_сonsultationService.ConsultatonExist(idConsultation))
            {
                return NotFound();
            }

            var сonsultation = _сonsultationService.Get(idConsultation);
            await FillViewBagPatientAsync();

            return View("Update", сonsultation);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Сonsultation сonsultation)
        {
            if (!_сonsultationService.ConsultatonExist(сonsultation.IdConsultation))
            {
                return NotFound();
            }

            _сonsultationService.Update(сonsultation);
            await FillViewBagPatientAsync();
            ViewBag.Information = "Данные пациента успешно изменены!";
            return View("Update", сonsultation);
        }

        public IActionResult Delete(long idConsultation)
        {
            if (!_сonsultationService.ConsultatonExist(idConsultation))
            {
                return NotFound();
            }

            _сonsultationService.Delete(idConsultation);
            return RedirectToAction("Journal");
        }
        
        private async Task FillViewBagPatientAsync()
        {
            var patiens = _сonsultationService.GetPatients();
            ViewBag.Patiens = new SelectList(await patiens.ToListAsync(), "IdPatient", "FIO");
        }
    }
}
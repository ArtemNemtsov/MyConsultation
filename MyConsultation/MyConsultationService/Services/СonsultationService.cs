using ConsultationService.PageModels;
using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultationService.Services
{
    public class СonsultationService
    {
        private readonly d6tp5on2hao81vContext _postgres;
        public СonsultationService(d6tp5on2hao81vContext postgres)
        {
            _postgres = postgres;
        }

        public long Create(Сonsultation consultation)
        {
            _postgres.Сonsultation.Add(consultation);
            _postgres.SaveChanges();
            return consultation.IdConsultation;
        }

        public IQueryable<ConsultationJournalModel> GetAll()
        {
            return _postgres.VJournalConsultation
                .AsNoTracking()
                .Take(100)
                .Select(s => new ConsultationJournalModel
                {
                    IdConsultation = s.IdConsultation,
                    Patient = s.FioPatient,
                    Date = s.Date.ToLongDateString(),
                    Time = s.Time.ToString(),
                });
        }
        public Сonsultation Get(long idConsultation)
        {
            return _postgres.Сonsultation.Find(idConsultation);
        }

        public IQueryable<PatientModel> GetPatients()
        {
            return _postgres.Patient
                .AsNoTracking()
                .OrderBy(s => s.Surname)
                .Select(s => new PatientModel
                {
                    IdPatient = s.IdPatient,
                    FIO = $"{s.Surname} {s.Name} {s.MiddleName}", 
                });
        }

        public long Update(Сonsultation consultation)
        {
            _postgres.Сonsultation.Update(consultation);
            _postgres.SaveChanges();
            return consultation.IdConsultation;
        }

        public void Delete(long idConsultation)
        {
            var patient = _postgres.Сonsultation.Find(idConsultation);
            _postgres.Remove(patient);
            _postgres.SaveChanges();
        }

        public bool ConsultatonExist(long idConsultation)
        {
            return _postgres.Сonsultation.Any(w => w.IdConsultation == idConsultation);
        }
    }
}

using ConsultationService.Core.Classes;
using ConsultationService.PageModels;
using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultationService.Services
{
    public class PatientService
    {
        private readonly d6tp5on2hao81vContext _postgres;
        public PatientService(d6tp5on2hao81vContext postgres)
        {
            _postgres = postgres;
        }

        public IQueryable<PatientJournalModel> GetAll()
        {
            return _postgres.Patient
                .AsNoTracking()
                .Take(100)
                .Select(s => new PatientJournalModel
                { 
                    IdPatient = s.IdPatient,
                    FIO = $"{s.Surname} {s.Name} {s.MiddleName}",
                    Birthdate = s.Birthdate.ToLongDateString(),
                    Gender = s.Gender,
                    Snils = s.Snils,
                });
        }

        public IQueryable<PatientJournalModel> FindBySnils(string searshSnils)
        {
            var patiens = GetAll();

            if (!String.IsNullOrEmpty(searshSnils))
            {
                patiens = patiens.Where(s => s.Snils.Contains(searshSnils));
            }
            return patiens;
        }

        public IQueryable<PatientJournalModel> FindByFIO(string FIO)
        {
            var patiens = GetAll();

            if (!String.IsNullOrEmpty(FIO))
            {
                patiens = patiens.Where(s => s.FIO.Contains(FIO));
            }
            return patiens;
        }

        public int Save(Patient patient)
        {
            _postgres.Patient.Add(patient);
            _postgres.SaveChanges();
            return patient.IdPatient;
        }
        public bool CheckSnilsValid(string dirtySnils)
        {
            var snils = SnilsBuilder.BuldSnils(dirtySnils);

            return snils.IsValid;
        }

        public bool SnilsExist(string snils)
        {
            return _postgres.Patient              
                .Where(w => w.Snils == snils)
                .Any();
        }
    }
}

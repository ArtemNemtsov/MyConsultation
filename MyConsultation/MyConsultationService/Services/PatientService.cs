using ConsultationService.Core.Classes;
using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _postgres.Patient.AsNoTracking().Take(100).ToListAsync();
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
    }
}

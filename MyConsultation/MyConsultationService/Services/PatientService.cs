using ConsultationService.Core.Classes;
using ConsultationService.PageModels;
using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
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

        public void Delete(int idPatient)
        {
            var patient = _postgres.Patient.Find(idPatient);

            var consultations = _postgres.Сonsultation
                .Where(w => w.IdPatient == idPatient);

            _postgres.RemoveRange(consultations);
            _postgres.Remove(patient);
            _postgres.SaveChanges();
        }

        public void Edit(int idPatient)
        {
            var patient = _postgres.Patient.Find(idPatient);
            _postgres.Remove(patient);
            _postgres.SaveChanges();
        }

        public Patient Get(int idPatient)
        {
            return PatientExist(idPatient) 
                ? _postgres.Patient.Find(idPatient) 
                : null;
        }

        public async Task<PatientDetailModel> GetDetailsAsync(int idPatient)
        {
               var patientDetail = await _postgres.VJournalPatient
                .AsNoTracking()
                .Where(w => w.IdPatient == idPatient)
                .Select(s => new PatientDetailModel
                {
                    IdPatient = s.IdPatient,
                    FullName = s.Fio,
                    Gender = s.Gender,
                    Snils = s.Snils,
                    Age =  DateTime.Now.DayOfYear < s.Birthdate.DayOfYear
                    ? DateTime.Today.Year - s.Birthdate.Year - 1
                    : DateTime.Today.Year - s.Birthdate.Year
   ,
                }).FirstOrDefaultAsync();
   
            patientDetail.Consultations = await GetConsultationAsync(idPatient);
            return patientDetail;
        }

        private async Task<List<Сonsultation>> GetConsultationAsync(int idPatient)
        {
            return await _postgres.Сonsultation
                .AsNoTracking()
                .Where(s => s.IdPatient == idPatient)
                .ToListAsync();
        }

        public IQueryable<PatientJournalModel> GetAll()
        {
            return _postgres.VJournalPatient
                .AsNoTracking()
                .Take(100)
                .Select(journalPatient => new PatientJournalModel
                { 
                    IdPatient = journalPatient.IdPatient,
                    FIO = journalPatient.Fio,
                    Birthdate = journalPatient.Birthdate.ToLongDateString(),
                    Gender = journalPatient.Gender,
                    Snils = journalPatient.Snils,
                });
        }

        public IQueryable<PatientJournalModel> FindBySnils(string searshSnils)
        {
            return String.IsNullOrEmpty(searshSnils)
                ? GetAll()
                : GetAll()
                    .Where(patientJournal => patientJournal.Snils.Contains(searshSnils));
        }

        public IQueryable<PatientJournalModel> FindByFIO(string FIO)
        {
            return String.IsNullOrEmpty(FIO)
                ? GetAll()
                : GetAll()
                    .Where(patientJournal => patientJournal.FIO.Contains(FIO));
        }

        public int Create(Patient patient)
        {
            _postgres.Patient.Add(patient);
            _postgres.SaveChanges();
            return patient.IdPatient;
        }

        public int Update(Patient patient)
        {
            _postgres.Patient.Update(patient);
            _postgres.SaveChanges();
            return patient.IdPatient;
        }

        public bool CheckSnilsValid(string dirtySnils)
        {
            var snils = SnilsBuilder.BuldSnils(dirtySnils);

            if (!snils.IsValid)
                throw new Exception($"Введенные данные СНИЛС некоректны, попробуйте снова!");

            return true;
        }

        public bool CheckSnils(string newSnils, int? idPatient)
        {
            // Если редактировали пациента
            if (idPatient.HasValue)
            {
                var oldSnils = _postgres.Patient.Find(idPatient).Snils;

                // Но СНИЛС остался прежним, то все ок
                if (oldSnils == newSnils)
                    return true;          
            }

            // проверяем корректность СНИЛС
            CheckSnilsValid(newSnils);

            // проверяем уникальность СНИЛС 
            if (CheckSnilsExist(newSnils))
                throw new Exception($"Данный СНИЛС уже заведен в БД!");

            return true;
        }

        private bool CheckSnilsExist(string newSnils)
        {
            return _postgres.Patient
                .Where(w => w.Snils == newSnils)
                .Any();
        }

        public bool PatientExist(int idPatient)
        {
            return _postgres.Patient.Any(w => w.IdPatient == idPatient);
        }
    }
}

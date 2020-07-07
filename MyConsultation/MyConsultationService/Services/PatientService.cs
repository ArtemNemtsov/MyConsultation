using ConsultationService.Core.Classes;
using ConsultationService.PageModels;
using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        public IQueryable<PatientJournalModel> GetAll()
        {
            return _postgres.VJournalPatient
                .AsNoTracking()
                .Take(100)
                .Select(s => new PatientJournalModel
                { 
                    IdPatient = s.IdPatient,
                    FIO = s.Fio,
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
            // проверяем корректность СНИЛС
            CheckSnilsValid(newSnils);

            // Если редактировали пациента
            if (idPatient.HasValue)
            {
                var oldSnils = _postgres.Patient.Find(idPatient).Snils;

                // Но СНИЛС остался прежним, то все ок
                if (oldSnils == newSnils)
                    return true;          
            }
            
            // проверяем, что введденый СНИЛС уникальный
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

using DBContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace ConsultationService.PageModels
{
    public class PatientDetailModel
    {
        public int IdPatient { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Display(Name = "Снилс")]
        public string Snils { get; set; }

        [DisplayFormat(NullDisplayText = "Данные отсуствуют")]
        public List <Сonsultation> Consultations { get; set; }
    }
}

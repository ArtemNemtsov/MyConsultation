using DBContext.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

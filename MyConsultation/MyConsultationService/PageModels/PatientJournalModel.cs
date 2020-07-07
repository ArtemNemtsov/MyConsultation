using System.ComponentModel.DataAnnotations;

namespace ConsultationService.PageModels
{
    public class PatientJournalModel
    {
        public int IdPatient { get; set; }

        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Display(Name = "Дата рождения")]
        public string Birthdate { get; set; }

        [Display(Name = "СНИЛС")]
        public string Snils { get; set; }
    }
}

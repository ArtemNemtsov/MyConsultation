using System.ComponentModel.DataAnnotations;

namespace ConsultationService.PageModels
{
    public class ConsultationJournalModel
    {
        public long IdConsultation { get; set; }

        [Display(Name = "Пациент")]
        public string Patient { get; set; }

        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Время")]
        public string Time { get; set; }
    }
}

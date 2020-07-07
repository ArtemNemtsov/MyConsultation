using System;

namespace DBContext.Models
{
    public partial class VJournalConsultation
    {
        public long IdConsultation { get; set; }
        public string FioPatient { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Symptoms { get; set; }
    }
}

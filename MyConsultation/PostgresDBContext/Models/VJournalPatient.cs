using System;

namespace DBContext.Models
{
    public partial class VJournalPatient
    {
        public int IdPatient { get; set; }
        public string Fio { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Snils { get; set; }
    }
}

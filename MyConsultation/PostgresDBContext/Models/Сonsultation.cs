using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Сonsultation
    {
        public long IdConsultation { get; set; }
        public int IdPatient { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Symptoms { get; set; }

        public virtual Patient IdPatientNavigation { get; set; }
    }
}

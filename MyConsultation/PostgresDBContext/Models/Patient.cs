using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Сonsultation = new HashSet<Сonsultation>();
        }

        public int IdPatient { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string Snils { get; set; }

        public virtual ICollection<Сonsultation> Сonsultation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string FIO { get; set; }
        public int IIN { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public MedicalCard MedicalCard { get; set; }
    }
}

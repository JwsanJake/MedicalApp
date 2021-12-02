using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }

        public int MC_ID { get; set; }
        public string DocFIO { get; set; }
        public string DocSpec { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string Сomplaints { get; set; }

        [ForeignKey("MC_ID")]
        public MedicalCard MedicalCard { get; set; }


    }
}

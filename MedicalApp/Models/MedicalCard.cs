using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class MedicalCard
    {
        [Key]
        public int MC_ID { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public IEnumerable<Visit> Visit { get; set; }
    }
}

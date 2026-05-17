using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class Hospitalization
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; } = null!;

        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        public DateTime? AdmissionDate { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "InQueue"; // InQueue, Admitted, Discharged, Cancelled
    }
}

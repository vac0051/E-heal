using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthDesktop.Models
{
    public class Hospitalization
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;
        public DateTime? AdmissionDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "InQueue";
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled
    }
}

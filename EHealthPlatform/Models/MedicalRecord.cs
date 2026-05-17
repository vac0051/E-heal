using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public List<Prescription> Prescriptions { get; set; } = new();
    }
}

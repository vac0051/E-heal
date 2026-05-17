using System;
using System.Collections.Generic;

namespace EHealthAvalonia.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Diagnosis { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;
        public List<Prescription> Prescriptions { get; set; } = new();
    }
}

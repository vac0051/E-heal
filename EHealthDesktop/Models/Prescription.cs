using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthDesktop.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; } = null!;
        public string Medication { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
    }
}

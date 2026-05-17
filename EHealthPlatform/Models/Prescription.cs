using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public int MedicalRecordId { get; set; }
        [ForeignKey("MedicalRecordId")]
        public MedicalRecord MedicalRecord { get; set; } = null!;

        [Required]
        public string Medication { get; set; } = string.Empty;

        [Required]
        public string Dosage { get; set; } = string.Empty;
    }
}

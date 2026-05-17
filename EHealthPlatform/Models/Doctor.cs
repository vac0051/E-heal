using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; } = string.Empty;

        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; } = null!;
    }
}

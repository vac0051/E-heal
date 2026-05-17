using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthDesktop.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Specialty { get; set; } = string.Empty;
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;
    }
}

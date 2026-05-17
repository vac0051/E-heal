using System.ComponentModel.DataAnnotations;

namespace EHealthPlatform.Models
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}

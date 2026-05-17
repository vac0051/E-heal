using System.ComponentModel.DataAnnotations;

namespace EHealthPlatform.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        [Required]
        [StringLength(12)]
        public string Snils { get; set; } = string.Empty;
    }
}

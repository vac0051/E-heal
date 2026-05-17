using System.ComponentModel.DataAnnotations;

namespace EHealthAvalonia.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Snils { get; set; } = string.Empty;
    }
}

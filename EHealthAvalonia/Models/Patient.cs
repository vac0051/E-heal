using System;

namespace EHealthAvalonia.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string InsurancePolicy { get; set; } = string.Empty;
    }
}

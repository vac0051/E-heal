using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHealthPlatform.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(50)]
        public string InsurancePolicy { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? StudentMajor { get; set; }

        [Required]
        public string? StudentGradYear { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}

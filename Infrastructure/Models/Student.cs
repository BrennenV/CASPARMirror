using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("StudentMajor")]
        public string? StudentMajor { get; set; }

        [Required]
        [DisplayName("Student Grad Year")]
        public string? StudentGradYear { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}

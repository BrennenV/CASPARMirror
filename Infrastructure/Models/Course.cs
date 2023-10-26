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
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CourseTitle { get; set; }

        [Required]
        public int CourseCreditHours { get; set; }

        [Required]
        public string? CourseNumber { get; set; }

        public string? CourseDescription { get; set; }

        public string? CourseNotes { get; set; }

        [Required]
        [DisplayName("Program")]
        public int ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public AcademicProgram? AcademicProgram { get; set; }
    }
}

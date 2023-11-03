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
        [DisplayName("Course Title")]
        public string? CourseTitle { get; set; }

        [Required]
        [DisplayName("Course Credit Hours")]
        public int CourseCreditHours { get; set; }

        [Required]
        [DisplayName("Course Number")]
        public string? CourseNumber { get; set; }

        public string? CourseDescription { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Program")]
        public int ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public AcademicProgram? AcademicProgram { get; set; }
    }
}

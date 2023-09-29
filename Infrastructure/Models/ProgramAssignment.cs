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
    public class ProgramAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Instructor")]
        public int InstructorId { get; set; }

        [Required]
        [DisplayName("Program")]
        public int ProgramId { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor? Instructor { get; set; }

        [ForeignKey("ProgramId")]
        public AcademicProgram? Program { get; set; }
    }
}

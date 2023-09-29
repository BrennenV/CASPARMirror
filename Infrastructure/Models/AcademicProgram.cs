using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AcademicProgram
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ProgramName { get; set; }

        [Required]
        public string? ProgramCode { get; set; }
    }
}

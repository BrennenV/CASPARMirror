using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Program Name")]
        public string? ProgramName { get; set; }

        [Required]
        [DisplayName("Program Code")]
        public string? ProgramCode { get; set; }

        public bool? IsArchived { get; set; }
    }
}

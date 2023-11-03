using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Semester Name")]
        public string? SemesterName { get; set; }

        public bool? IsArchived { get; set; }
    }
}

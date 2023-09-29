using System;
using System.Collections.Generic;
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
        public string SemesterName { get; set; }


        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime? RegistrationDate { get; set; }

        [Required]
        public DateTime? EndRegistrationDate { get; set; }
    }
}

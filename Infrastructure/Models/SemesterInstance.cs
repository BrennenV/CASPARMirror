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
    public class SemesterInstance
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string? SemesterInstanceName { get; set; }

        [Required]
        public string? ScheduleStatus { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime? RegistrationDate { get; set; }

        [Required]
        public DateTime? EndRegistrationDate { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        [ForeignKey("SemesterId")]
        public Semester? Semester { get; set; }
    }
}

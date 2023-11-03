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
        [DisplayName("Semester Instance Name")]
        public string? SemesterInstanceName { get; set; }

        [Required]
        [DisplayName("Schedule Status")]
        public string? ScheduleStatus { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [DisplayName("Registration Date")]
        public DateTime? RegistrationDate { get; set; }

        [Required]
        [DisplayName("End Registration Date")]
        public DateTime? EndRegistrationDate { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        [ForeignKey("SemesterId")]
        public Semester? Semester { get; set; }
    }
}

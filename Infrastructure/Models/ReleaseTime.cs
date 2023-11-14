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
    public class ReleaseTime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Release Time Amount")]
        public int ReleaseTimeAmount { get; set; }

        public string? ReleaseTimeNotes { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Semester Instance")]
        public int SemesterInstanceId { get; set; }

        [Required]
        [DisplayName("Instructor")]
        public string? InstructorId { get; set; }

        [ForeignKey("SemesterInstanceId")]
        public SemesterInstance? SemesterInstance { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}

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
    public class Template
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [ForeignKey("SemesterId")]
        public Semester? Semester { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}

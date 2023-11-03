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
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Classroom Detail")]
        public string? ClassroomDetail { get; set; }

        [Required]
        [DisplayName("Classroom Seats")]
        public int ClassroomSeats { get; set; }

        [Required]
        [DisplayName("Classroom Number")]
        public string? ClassroomNumber { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Building")]
        public int BuildingId { get; set; }

        [ForeignKey("BuildingId")]
        public Building? Building { get; set; }
    }
}

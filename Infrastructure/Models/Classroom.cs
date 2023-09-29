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
        public string? ClassroomDetail { get; set; }

        [Required]
        public int ClassroomSeats { get; set; }

        [Required]
        public string ClassroomNumber { get; set; }

        [Required]
        [DisplayName("Building")]
        public int BuildingId { get; set; }

        [ForeignKey("BuildingId")]
        public Building? Building { get; set; }
    }
}

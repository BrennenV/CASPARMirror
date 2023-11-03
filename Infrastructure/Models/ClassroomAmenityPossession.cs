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
    public class ClassroomAmenityPossession
    {
        [Key]
        public int Id { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Classroom Amenity")]
        public int ClassroomAmenityId { get; set; }

        [Required]
        [DisplayName("Classroom")]
        public int ClassroomId { get; set; }

        [ForeignKey("ClassroomAmenityId")]
        public ClassroomAmenity? ClassroomAmenity { get; set; }

        [ForeignKey("ClassroomId")]
        public Classroom? Classroom { get; set; }
    }
}

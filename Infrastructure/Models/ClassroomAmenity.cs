using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ClassroomAmenity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Classroom Amenity Name")]
        public string? ClassroomAmenityName { get; set; }

        public bool? IsArchived { get; set; }
    }
}

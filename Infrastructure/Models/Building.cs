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
    public class Building
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Building Name")]
        public string? BuildingName { get; set; }

        public bool? IsArchived { get; set; }

        [Required]
        [DisplayName("Campus")]
        public int CampusId { get; set; }

        [ForeignKey("CampusId")]
        public Campus? Campus { get; set; }
    }
}

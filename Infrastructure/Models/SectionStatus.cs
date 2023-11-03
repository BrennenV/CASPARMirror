using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class SectionStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Section Status Name")]
        public string? SectionStatusName { get; set; }

        [Required]
        [DisplayName("Section Status Color")]
        public string? SectionStatusColor { get; set; }

        public bool? IsArchived { get; set; }
    }
}

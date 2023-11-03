using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PayOrganization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Pay Organization Title")]
        public string? PayOrganizationTitle { get; set; }

        public bool? IsArchived { get; set; }
    }
}

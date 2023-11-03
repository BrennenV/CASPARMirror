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
    public class TimeBlock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Time Block Value")]
        public string? TimeBlockValue { get; set; }

        public bool? IsArchived { get; set; }
    }
}

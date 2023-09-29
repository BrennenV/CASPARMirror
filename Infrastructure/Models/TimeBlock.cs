using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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
        public string? TimeBlockName { get; set; }

        [Required]
        public string? TimeBlockStart { get; set; }

        [Required]
        public string? TimeBlockEnd { get; set;}
    }
}

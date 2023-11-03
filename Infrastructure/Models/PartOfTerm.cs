using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PartOfTerm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Part of Term Title")]
        public string? PartOfTermTitle { get; set; }

        public bool? IsArchived { get; set; }
    }
}

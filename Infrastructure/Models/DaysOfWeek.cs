using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class DaysOfWeek
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DaysOfWeekTitle { get; set; }
    }
}

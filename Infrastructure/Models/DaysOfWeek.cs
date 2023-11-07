﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Days of Week")]
        public string? DaysOfWeekValue { get; set; }

        public bool? IsArchived { get; set; }
    }
}

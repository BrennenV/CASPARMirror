﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class LoadReq
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LoadReqHours { get; set; }

        [Required]
        [DisplayName("Instructor")]
        public int InstructorId { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor? Instructor { get; set; }

        [ForeignKey("SemesterId")]
        public Semester? Semester { get; set; }
    }
}
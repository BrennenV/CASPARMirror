﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
	public class TimeOfDay
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("Time of Day")]
		public string? PartOfDay { get; set; }
	}
}

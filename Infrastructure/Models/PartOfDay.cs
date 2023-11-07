using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
	public class PartOfDay
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("Part of Day")]
		public string? PartOfDayValue { get; set; }

		public bool? IsArchived { get; set; }
	}
}

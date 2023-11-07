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
	public class WishlistDaysOfWeek
	{
		[Key]
		public int Id { get; set; }

		public bool? IsArchived { get; set; }

		[Required]
		[DisplayName("Wishlist")]
		public int WishlistId { get; set; }

		[Required]
		[DisplayName("Days of the Week")]
		public int DaysOfWeekId { get; set; }

		[ForeignKey("WishlistId")]
		public Wishlist? Wishlist { get; set; }

		[ForeignKey("DaysOfWeekId")]
		public DaysOfWeek? DaysOfWeek { get; set; }
	}
}

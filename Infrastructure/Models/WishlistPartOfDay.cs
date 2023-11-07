using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
	public class WishlistPartOfDay
	{
		[Key]
		public int Id { get; set; }

		public bool? IsArchived { get; set; }

		[Required]
		[DisplayName("Wishlist")]
		public int WishlistId { get; set; }

		[Required]
		[DisplayName("Part of Day")]
		public int PartOfDayId { get; set; }

		[ForeignKey("WishlistId")]
		public Wishlist? Wishlist { get; set; }

		[ForeignKey("PartOfDayId")]
		public PartOfDay? PartOfDay { get; set; }
	}
}

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
	public class WishlistCourse
	{
		[Key]
		public int Id { get; set; }

		[DisplayName("Preference Rank")]
		public int? PreferenceRank { get; set; }

		public bool? IsArchived { get; set; }

		[Required]
		[DisplayName("Wishlist")]
		public int WishlistId { get; set; }

		[Required]
		[DisplayName("Course")]
		public int CourseId { get; set; }

		[ForeignKey("WishlistId")]
		public Wishlist? Wishlist { get; set; }

		[ForeignKey("CourseId")]
		public Course? Course { get; set; }
	}
}

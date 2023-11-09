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
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        public bool? IsArchived { get; set; }

		[Required]
        [DisplayName("User")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Semester Instance")]
        public int SemesterInstanceId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("SemesterInstanceId")]
        public SemesterInstance? SemesterInstance { get; set; }
    }
}

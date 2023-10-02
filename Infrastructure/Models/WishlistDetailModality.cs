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
    public class WishlistDetailModality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? WishlistPartOfDay { get; set; }

        [Required]
        [DisplayName("Wishlist Detail")]
        public int WishlistDetailId { get; set; }

        [Required]
        [DisplayName("Modality")]
        public int ModalityId { get; set; }

        [Required]
        [DisplayName("Days of the Week")]
        public int DaysOfWeekId { get; set; }

        [ForeignKey("WishlistDetailId")]
        public WishlistDetail? WishlistDetail { get; set; }

        [ForeignKey("ModalityId")]
        public Modality? Modality { get; set; }

        [ForeignKey("DaysOfWeekId")]
        public DaysOfWeek? DaysOfWeek { get; set; }
    }
}

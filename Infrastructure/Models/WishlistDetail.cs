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
    public class WishlistDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? WishlistPartOfDay { get; set; }

        [Required]
        [DisplayName("Wishlist")]
        public int WishlistId { get; set; }

        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Modality")]
        public int ModalityId { get; set; }

        [Required]
        [DisplayName("Days of the Week")]
        public int DaysOfWeekId { get; set; }

        [ForeignKey("WishlistId")]
        public Wishlist? Wishlist { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [ForeignKey("ModalityId")]
        public Modality? Modality { get; set; }

        [ForeignKey("DaysOfWeekId")]
        public DaysOfWeek? DaysOfWeek { get; set; }
    }
}

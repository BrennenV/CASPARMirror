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

        [Required]
        public string? WishlistYear { get; set; }

        [Required]
        [DisplayName("Student")]
        public int StudentId { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [ForeignKey("SemesterId")]
        public Semester? Semester { get; set; }
    }
}

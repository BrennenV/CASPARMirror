//using Microsoft.Identity.Client;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Models
//{
//    public class Instructor
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        [DisplayName("Instructor Name")]
//        public string? InstructorName { get; set; }

//        public bool? IsArchived { get; set; }

//        [Required]
//        [DisplayName("User")]
//        public int UserId { get; set; }

//        [ForeignKey("UserId")]
//        public User? User { get; set; }
//    }
//}

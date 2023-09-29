using Microsoft.Identity.Client;
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
    public class RoleAssignment
    {
        //THIS MODEL SHOULD EVENTUALLY BE REPLACED
        [Key]
        public string Id { get; set; }

        [Required]
        [DisplayName("Role")]
        public int RoleId { get; set; }

        [Required]
        [DisplayName("User")]
        public int UserId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}

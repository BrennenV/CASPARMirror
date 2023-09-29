using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class User
    {
        //THIS MODEL SHOULD EVENTUALLY BE REPLACED
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required] 
        public string UserLastName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}

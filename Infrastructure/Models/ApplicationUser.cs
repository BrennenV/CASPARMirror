using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    [DisplayName("First Name")]
    public string? FirstName { get; set; }

    [Required]
    [DisplayName("Last Name")]
    public string? LastName { get; set; }

    [NotMapped]
    public string FullName { get { return FirstName + " " + LastName; } }
}


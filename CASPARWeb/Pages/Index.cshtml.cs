using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace CASPARWeb.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
			if (User.IsInRole(SD.ADMIN_ROLE))
			{
                return Page();
			}

            if(User.IsInRole(SD.PROGRAM_COORDINATOR_ROLE)) 
            {
                return Redirect("/Coord/BuildSchedule/Index");
            }

			if (User.IsInRole(SD.INSTRUCTOR_ROLE))
            {
                return Redirect("/Instr/Wishlists/Index");
            }

            if (User.IsInRole(SD.STUDENT_ROLE))
            {
                return Redirect("/Stud/Wishlists/Index");
            }

			return Page();
        }
    }
}
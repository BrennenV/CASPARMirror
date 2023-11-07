using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Stud.Pages.WishListsOLD
{
    public class IndexModel : PageModel
    {
		public int SelectedSemesterId;
		public IActionResult OnGet(int? selectedSemesterId)
		{
			//This code is to help preserve the selected semester id upon returning to the index page
			if (selectedSemesterId != null)
			{
				SelectedSemesterId = (int)selectedSemesterId;
			}
			else
			{
				SelectedSemesterId = 0;
			}

			return Page();
		}
	}
}

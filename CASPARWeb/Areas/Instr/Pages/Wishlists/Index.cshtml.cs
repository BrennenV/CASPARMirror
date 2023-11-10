using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Instr.Pages.Wishlists
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

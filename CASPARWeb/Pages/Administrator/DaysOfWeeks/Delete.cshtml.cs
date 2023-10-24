using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.DaysOfWeeks
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public DaysOfWeek objDaysOfWeek { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objDaysOfWeek = new DaysOfWeek();
        }
        public IActionResult OnGet(int? id)
        {

            if (id != null && id != 0)
            {
                objDaysOfWeek = _unitOfWork.DaysOfWeek.GetById(id);
            }
            //Nothing found in DB
            if (objDaysOfWeek == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objDaysOfWeek = _unitOfWork.DaysOfWeek.GetById(id);
            if (objDaysOfWeek == null)
            {
                return NotFound();
            }
            _unitOfWork.DaysOfWeek.Delete(objDaysOfWeek);
            TempData["success"] = "Days of the Week Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

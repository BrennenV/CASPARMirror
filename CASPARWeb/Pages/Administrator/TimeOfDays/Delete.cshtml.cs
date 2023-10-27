using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.TimeOfDays
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public TimeOfDay objTimeOfDay { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objTimeOfDay = new TimeOfDay();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objTimeOfDay = _unitOfWork.TimeOfDay.GetById(id);
            }
            //Nothing found in DB
            if (objTimeOfDay == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objTimeOfDay = _unitOfWork.TimeOfDay.GetById(id);
            if (objTimeOfDay == null)
            {
                return NotFound();
            }
            _unitOfWork.TimeOfDay.Delete(objTimeOfDay);
            TempData["success"] = "Time Of Day Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

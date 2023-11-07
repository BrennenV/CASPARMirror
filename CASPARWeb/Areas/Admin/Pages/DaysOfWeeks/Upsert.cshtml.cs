using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.DaysOfWeeks
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public DaysOfWeek objDaysOfWeek { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objDaysOfWeek = new DaysOfWeek();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objDaysOfWeek = _unitOfWork.DaysOfWeek.GetById(id);
            }
            //Nothing found in DB
            if (objDaysOfWeek == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            //Creating a Row
            if (objDaysOfWeek.Id == 0)
            {
                _unitOfWork.DaysOfWeek.Add(objDaysOfWeek);
                TempData["success"] = "Days of the Week Choice added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.DaysOfWeek.Update(objDaysOfWeek);
                TempData["success"] = "Days of the Week Choice updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

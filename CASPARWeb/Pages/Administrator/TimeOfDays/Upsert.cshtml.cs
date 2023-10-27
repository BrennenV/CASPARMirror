using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.TimeOfDays
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public TimeOfDay objTimeOfDay { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objTimeOfDay = new TimeOfDay();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objTimeOfDay = _unitOfWork.TimeOfDay.GetById(id);
            }
            //Nothing found in DB
            if (objTimeOfDay == null)
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
            if (objTimeOfDay.Id == 0)
            {
                _unitOfWork.TimeOfDay.Add(objTimeOfDay);
                TempData["success"] = "Time Of Day added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.TimeOfDay.Update(objTimeOfDay);
                TempData["success"] = "Time Of Day updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

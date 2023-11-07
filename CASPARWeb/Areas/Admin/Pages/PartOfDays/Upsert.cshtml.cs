using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.PartOfDays
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PartOfDay objPartOfDay { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPartOfDay = new PartOfDay();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objPartOfDay = _unitOfWork.PartOfDay.GetById(id);
            }
            //Nothing found in DB
            if (objPartOfDay == null)
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
            if (objPartOfDay.Id == 0)
            {
                _unitOfWork.PartOfDay.Add(objPartOfDay);
                TempData["success"] = "Time Of Day added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PartOfDay.Update(objPartOfDay);
                TempData["success"] = "Time Of Day updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.TimeBlocks
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;//easy to use, database managment methods
        [BindProperty]
        public TimeBlock objTimeBlock { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objTimeBlock = new TimeBlock();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objTimeBlock = _unitOfWork.TimeBlock.GetById(id);
            }
            //Nothing found in DB
            if (objTimeBlock == null)
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
            if (objTimeBlock.Id == 0)
            {
                _unitOfWork.TimeBlock.Add(objTimeBlock);
                TempData["success"] = "Time Block added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.TimeBlock.Update(objTimeBlock);
                TempData["success"] = "Time Block updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

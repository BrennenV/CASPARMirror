using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Administrator.PartOfTerms
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PartOfTerm objPartOfTerm { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPartOfTerm = new PartOfTerm();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objPartOfTerm = _unitOfWork.PartOfTerm.GetById(id);
            }
            //Nothing found in DB
            if (objPartOfTerm == null)
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
            if (objPartOfTerm.Id == 0)
            {
                _unitOfWork.PartOfTerm.Add(objPartOfTerm);
                TempData["success"] = "Part of Term added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PartOfTerm.Update(objPartOfTerm);
                TempData["success"] = "Part of Term updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.PartOfTerms
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PartOfTerm objPartOfTerm { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPartOfTerm = new PartOfTerm();
        }
        public IActionResult OnGet(int? id)
        {

            if (id != null && id != 0)
            {
                objPartOfTerm = _unitOfWork.PartOfTerm.GetById(id);
            }
            //Nothing found in DB
            if (objPartOfTerm == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objPartOfTerm = _unitOfWork.PartOfTerm.GetById(id);
            if (objPartOfTerm == null)
            {
                return NotFound();
            }
            _unitOfWork.PartOfTerm.Delete(objPartOfTerm);
            TempData["success"] = "Part of Term Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

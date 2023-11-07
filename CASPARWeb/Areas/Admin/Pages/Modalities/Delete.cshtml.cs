using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.Modalities
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Modality objModality { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objModality = new Modality();
        }
        public IActionResult OnGet(int? id)
        {

            if (id != null && id != 0)
            {
                objModality = _unitOfWork.Modality.GetById(id);
            }
            //Nothing found in DB
            if (objModality == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objModality = _unitOfWork.Modality.GetById(id);
            if (objModality == null)
            {
                return NotFound();
            }
            _unitOfWork.Modality.Delete(objModality);
            TempData["success"] = "Modality Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

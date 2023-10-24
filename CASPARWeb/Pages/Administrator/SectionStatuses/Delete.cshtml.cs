using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.SectionStatuses
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SectionStatus objSectionStatus { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSectionStatus = new SectionStatus();
        }
        public IActionResult OnGet(int? id)
        {

            if (id != null && id != 0)
            {
                objSectionStatus = _unitOfWork.SectionStatus.GetById(id);
            }
            //Nothing found in DB
            if (objSectionStatus == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objSectionStatus = _unitOfWork.SectionStatus.GetById(id);
            if (objSectionStatus == null)
            {
                return NotFound();
            }
            _unitOfWork.SectionStatus.Delete(objSectionStatus);
            TempData["success"] = "Section Status Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

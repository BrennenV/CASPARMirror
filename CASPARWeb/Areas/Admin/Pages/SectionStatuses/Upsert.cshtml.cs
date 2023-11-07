using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.SectionStatuses
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SectionStatus objSectionStatus { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSectionStatus = new SectionStatus();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objSectionStatus = _unitOfWork.SectionStatus.GetById(id);
            }
            //Nothing found in DB
            if (objSectionStatus == null)
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
            if (objSectionStatus.Id == 0)
            {
                _unitOfWork.SectionStatus.Add(objSectionStatus);
                TempData["success"] = "Section Status added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.SectionStatus.Update(objSectionStatus);
                TempData["success"] = "Section Status updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

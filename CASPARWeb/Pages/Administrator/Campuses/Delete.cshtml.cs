using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Campuses
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Campus objCampus { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objCampus = new Campus();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objCampus = _unitOfWork.Campus.GetById(id);
            }
            //Nothing found in DB
            if (objCampus == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objCampus = _unitOfWork.Campus.GetById(id);
            if (objCampus == null)
            {
                return NotFound();
            }
            _unitOfWork.Campus.Delete(objCampus);
            TempData["success"] = "Campus Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

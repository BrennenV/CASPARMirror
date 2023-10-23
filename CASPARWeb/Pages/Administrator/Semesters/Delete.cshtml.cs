using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Administrator.Semesters
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Semester objSemester { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemester = new Semester();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objSemester = _unitOfWork.Semester.GetById(id);
            }
            //Nothing found in DB
            if (objSemester == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objSemester = _unitOfWork.Semester.GetById(id);
            if (objSemester == null)
            {
                return NotFound();
            }
            _unitOfWork.Semester.Delete(objSemester);
            TempData["success"] = "Semester Template Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

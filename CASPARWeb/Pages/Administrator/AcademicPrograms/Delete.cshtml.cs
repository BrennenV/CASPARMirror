using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.AcademicPrograms
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public AcademicProgram objAcademicProgram { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objAcademicProgram = new AcademicProgram();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objAcademicProgram = _unitOfWork.AcademicProgram.GetById(id);
            }
            //Nothing found in DB
            if (objAcademicProgram == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objAcademicProgram = _unitOfWork.AcademicProgram.GetById(id);
            if (objAcademicProgram == null)
            {
                return NotFound();
            }
            _unitOfWork.AcademicProgram.Delete(objAcademicProgram);
            TempData["success"] = "Academic Program Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

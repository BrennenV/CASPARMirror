using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.SemesterInstances
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemesterInstance = new SemesterInstance();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objSemesterInstance = _unitOfWork.SemesterInstance.GetById(id);
            }
            //Nothing found in DB
            if (objSemesterInstance == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objSemesterInstance = _unitOfWork.SemesterInstance.GetById(id);
            if (objSemesterInstance == null)
            {
                return NotFound();
            }
            _unitOfWork.SemesterInstance.Delete(objSemesterInstance);
            TempData["success"] = "Semester Instance Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

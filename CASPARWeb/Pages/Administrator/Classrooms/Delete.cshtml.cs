using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Classrooms
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Classroom objClassroom { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objClassroom = new Classroom();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objClassroom = _unitOfWork.Classroom.GetById(id);
            }
            //Nothing found in DB
            if (objClassroom == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objClassroom = _unitOfWork.Classroom.GetById(id);
            if (objClassroom == null)
            {
                return NotFound();
            }
            _unitOfWork.Classroom.Delete(objClassroom);
            TempData["success"] = "Classroom Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Course objCourse { get; set; }
        public IEnumerable<SelectListItem> AcademicProgramList { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objCourse = new Course();
            AcademicProgramList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            AcademicProgramList = _unitOfWork.AcademicProgram.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.ProgramName,
                                Value = c.Id.ToString()
                            });
            //Edit mode
            if (id != null && id != 0)
            {
                objCourse = _unitOfWork.Course.GetById(id);
            }
            //Nothing found in DB
            if (objCourse == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objCourse= _unitOfWork.Course.GetById(id);
            if (objCourse == null)
            {
                return NotFound();
            }
            _unitOfWork.Course.Delete(objCourse);
            TempData["success"] = "Course Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

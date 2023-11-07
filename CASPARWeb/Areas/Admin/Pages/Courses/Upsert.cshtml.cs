using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.Courses
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Course objCourse { get; set; }
        public IEnumerable<SelectListItem> AcademicProgramList { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objCourse = new Course();
            AcademicProgramList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            AcademicProgramList = _unitOfWork.AcademicProgram.GetAll()
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
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            //Creating a Row
            if (objCourse.Id == 0)
            {
                _unitOfWork.Course.Add(objCourse);
                TempData["success"] = "Course added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.Course.Update(objCourse);
                TempData["success"] = "Course updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

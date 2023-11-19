using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.Semesters
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Semester objSemester { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemester = new Semester();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objSemester = _unitOfWork.Semester.GetById(id);
            }
            //Nothing found in DB
            if (objSemester == null)
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
            if (objSemester.Id == 0)
            {
                _unitOfWork.Semester.Add(objSemester);
                Expression<Func<Course, bool>> predicate = c => c.IsArchived != true;
                IEnumerable<Course> courses = _unitOfWork.Course.GetAll(predicate);
                foreach(Course course in courses)
                {
                    Template template = new Template();
                    template.Quantity = 0;
                    template.IsArchived = false;
                    template.Course = course;
                    template.Semester = objSemester;
                    _unitOfWork.Template.Add(template);
                }
                TempData["success"] = "Semester added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.Semester.Update(objSemester);
                TempData["success"] = "Semester updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("../Templates/Upsert", new { semesterId = objSemester.Id });
        }
    }
}

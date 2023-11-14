using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.Templates
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Template objTemplate { get; set; }
        public Course objCourse { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> SemesterList { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet(int? id,int semesterId)
        {
            objTemplate = new Template();
            CourseList = new List<SelectListItem>();
            SemesterList = new List<SelectListItem>();
            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = _unitOfWork.Course.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.CourseTitle,
                                Value = c.Id.ToString()
                            });
            SemesterList = _unitOfWork.Semester.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.SemesterName,
                                Value = c.Id.ToString()
                            });
            //Catch the semester id to use for new templates
            if (semesterId != 0)
            {
                objTemplate.SemesterId = semesterId;
            }
            objTemplate = _unitOfWork.Template.GetById(id);
            //Nothing found in DB
            if (objTemplate == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objTemplate = _unitOfWork.Template.GetById(id);
            if (objTemplate == null)
            {
                return NotFound();
            }
            _unitOfWork.Template.Delete(objTemplate);
            TempData["success"] = "Course Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new {id = objTemplate.SemesterId});
        }

    }
}

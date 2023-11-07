using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.Templates
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Template objTemplate { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> SemesterList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objTemplate = new Template();
            CourseList = new List<SelectListItem>();
            SemesterList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id,int semesterId)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = _unitOfWork.Course.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.CourseTitle,
                                Value = c.Id.ToString()
                            });
            SemesterList = _unitOfWork.Semester.GetAll()
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
            //Edit mode
            if (id != null && id != 0)
            {
                objTemplate = _unitOfWork.Template.GetById(id);
            }
            //Nothing found in DB
            if (objTemplate == null)
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
            if (objTemplate.Id == 0)
            {
                _unitOfWork.Template.Add(objTemplate);
                TempData["success"] = "Template added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.Template.Update(objTemplate);
                TempData["success"] = "Template updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new {id = objTemplate.SemesterId});
        }
    }
}

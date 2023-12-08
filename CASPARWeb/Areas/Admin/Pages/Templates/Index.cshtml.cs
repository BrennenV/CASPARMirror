using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.Templates
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Semester objSemester { get; set; }
        [BindProperty]
        public IEnumerable<Template> objTemplateList { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemester = new Semester();
            objTemplateList = new List<Template>();
            CourseList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //This is to make sure that when you go into edit templates, all courses are shown
            IEnumerable<Template> existingTemplates = _unitOfWork.Template.GetAll(c => c.IsArchived != true && c.SemesterId == id);
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll(c => c.IsArchived != true);
            List<int> existingCourseIds = new List<int>();

            foreach (var template in existingTemplates)
            {
                existingCourseIds.Add(template.CourseId);
            }

            foreach (var course in courses)
            {
                //Check if the semester had a template for the course
                //if not, add a new template
                if (!existingCourseIds.Contains(course.Id))
                {
                    Template newTemplate = new Template();
                    newTemplate.CourseId = course.Id;
                    newTemplate.SemesterId = (int)id;
                    newTemplate.Quantity = 0;

                    _unitOfWork.Template.Add(newTemplate);
                }
            }

            _unitOfWork.Commit();

            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = courses
                            .Select(c => new SelectListItem
                            {
                                Text = c.CourseTitle,
                                Value = c.Id.ToString()
                            });
            //objTemplateList = _unitOfWork.Template.GetAll(c => c.IsArchived != true);
            Expression<Func<Semester, bool>> predicate = c => c.Id == id && c.IsArchived != true;
            objSemester = _unitOfWork.Semester.Get(predicate, true, "Course,Semester");
            objTemplateList = _unitOfWork.Template.GetAll(c => c.SemesterId == id && c.IsArchived != true && c.Course.IsArchived != true, null, "Course,Semester,Course.AcademicProgram").OrderBy(c => c.Course.AcademicProgram.ProgramCode).ThenBy(c => c.Course.CourseNumber);
            return Page();
        }
        public IActionResult OnPost()
        {
            objSemester = _unitOfWork.Semester.Get(c => c.Id == objTemplateList.First().SemesterId);
            foreach (var objTemplate in objTemplateList)
            {
                objTemplate.IsArchived = false;
                objTemplate.Semester = _unitOfWork.Semester.Get(c => c.Id == objTemplate.SemesterId);
                objTemplate.Course = _unitOfWork.Course.Get(c => c.Id == objTemplate.CourseId);
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
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objSemester.Id });
        }
    }
}

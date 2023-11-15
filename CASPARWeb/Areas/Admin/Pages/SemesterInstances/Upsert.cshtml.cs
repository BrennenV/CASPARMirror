using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.SemesterInstances
{
	public class UpsertModel : PageModel
	{
		private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
		public CourseSection objCourseSection { get; set; }
		public IEnumerable<SelectListItem> SemesterList { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemesterInstance = new SemesterInstance();
            SemesterList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            SemesterList = _unitOfWork.Semester.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.SemesterName,
                                Value = c.Id.ToString()
                            });
            //Edit mode
            if (id != null && id != 0)
            {
                objSemesterInstance = _unitOfWork.SemesterInstance.GetById(id);
            }
            //Nothing found in DB
            if (objSemesterInstance == null)
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
            if (objSemesterInstance.Id == 0)
            {
				_unitOfWork.SemesterInstance.Add(objSemesterInstance);
				_unitOfWork.Commit();

				//get the <list> of templates that have a semesterId equal to the selected semesterId 
				IEnumerable<Template> templates = _unitOfWork.Template.GetAll(t => t.SemesterId == objSemesterInstance.SemesterId && t.IsArchived != true);

				//create all courseSections based on the templates
				foreach (Template template in templates)
				{
					if (template.Quantity > 0)
					{
						for (int i = 0; i < template.Quantity; i++)
						{
							//create the same course for each count in quantity
							objCourseSection = new CourseSection();
							objCourseSection.SemesterInstanceId = objSemesterInstance.Id;
							objCourseSection.CourseId = template.CourseId;
							objCourseSection.SectionUpdated = DateTime.Now;
							_unitOfWork.CourseSection.Add(objCourseSection);
						}
					}

				}
			}
            //Modifying a Row
            else
            {
                _unitOfWork.SemesterInstance.Update(objSemesterInstance);
                TempData["success"] = "Semester Instance updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Utility;

namespace CASPARWeb.Areas.Admin.Pages.SemesterInstances
{
	public class UpsertModel : PageModel
	{
		private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
		public CourseSection objCourseSection { get; set; }
		public IEnumerable<SelectListItem> SemesterList { get; set; }
        [BindProperty]
        public int oldSemesterValue { get; set; }
        public UpsertModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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
                oldSemesterValue = objSemesterInstance.SemesterId;
            }
            //Nothing found in DB
            if (objSemesterInstance == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public async Task<IActionResult> OnPost()
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
				await _unitOfWork.CommitAsync();

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

                IEnumerable<ApplicationUser> instructors = await _userManager.GetUsersInRoleAsync(SD.INSTRUCTOR_ROLE);
                foreach (var instructor in instructors)
                {
                    //Add default load requirements for every instructor
                    string currentSemesterType = _unitOfWork.Semester.GetById(objSemesterInstance.SemesterId).SemesterName;
                    LoadReq newLoadReq = new LoadReq();
                    newLoadReq.InstructorId = instructor.Id;
                    newLoadReq.SemesterInstanceId = objSemesterInstance.Id;

                    if (currentSemesterType == "Fall" || currentSemesterType == "Spring")
                    {
                        //Then the default load requirement time is 12
                        newLoadReq.LoadReqAmount = 12;
                    }
                    else
                    {
                        //Then the default value is 0
                        newLoadReq.LoadReqAmount = 0;
                    }

                    _unitOfWork.LoadReq.Add(newLoadReq);

                    //Add default Release Times for every instructor
                    ReleaseTime newReleaseTime = new ReleaseTime();
                    newReleaseTime.InstructorId = instructor.Id;
                    newReleaseTime.SemesterInstanceId = objSemesterInstance.Id;
                    newReleaseTime.ReleaseTimeAmount = 0;

                    _unitOfWork.ReleaseTime.Add(newReleaseTime);
                }

            }
            //Modifying a Row
            else
            {
                if(oldSemesterValue != objSemesterInstance.SemesterId)
                {
                    //If the semester template was changed, delete all the old course sections, and then add the new ones from the updated template
                    IEnumerable<CourseSection> courseSections = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == objSemesterInstance.Id && c.IsArchived != true);
                    foreach(CourseSection courseSection in courseSections)
                    {
                        _unitOfWork.CourseSection.Delete(courseSection);
                    }
                    await _unitOfWork.CommitAsync();

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
                _unitOfWork.SemesterInstance.Update(objSemesterInstance);
                TempData["success"] = "Semester Instance updated Successfully";
            }
            //Saves changes
            await _unitOfWork.CommitAsync();
            return RedirectToPage("./Index");
        }
    }
}

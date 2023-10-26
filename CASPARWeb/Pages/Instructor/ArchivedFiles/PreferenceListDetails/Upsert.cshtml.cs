using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetails
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetail objPreferenceListDetail { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetail = new PreferenceListDetail();
            CourseList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id, int preferenceListId)
        {
            //Capture the PreferenceList Id to use for new details
            if (preferenceListId != 0)
            {
                objPreferenceListDetail.PreferenceListId = preferenceListId;
            }
            //Edit mode
            if (id != null && id != 0)
            {
                objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceListDetail == null)
            {
                return NotFound();
            }

            //Get the current semester instance id so that we can figure out which courses are available for the semester instance
            int currentSemesterInstanceId = _unitOfWork.PreferenceList.GetById(objPreferenceListDetail.PreferenceListId).SemesterInstanceId;
            //Grab all the course ids that are currently available during the preference list's semester instance
            CourseList = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == currentSemesterInstanceId, null, "Course,SemesterInstance")
                            .Select(c => new SelectListItem
                            {
                                Text = c.Course.CourseTitle,
                                Value = c.CourseId.ToString()
                            });

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
            if (objPreferenceListDetail.Id == 0)
            {
                _unitOfWork.PreferenceListDetail.Add(objPreferenceListDetail);
                TempData["success"] = "Preference List added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PreferenceListDetail.Update(objPreferenceListDetail);
                TempData["success"] = "Preference List updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objPreferenceListDetail.PreferenceListId });
        }
    }
}

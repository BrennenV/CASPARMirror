using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.ArchivedFiles
{
    public class UpsertModelOLD : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceList objPreferenceList { get; set; }
        public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }

        public UpsertModelOLD(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceList = new PreferenceList();
            SemesterInstanceList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id, int semesterInstanceId)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            SemesterInstanceList = _unitOfWork.SemesterInstance.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.SemesterInstanceName,
                                Value = c.Id.ToString()
                            });
            //Catch the semester id to use for new templates
            if (semesterInstanceId != 0)
            {
                objPreferenceList.SemesterInstanceId = semesterInstanceId;
            }
            //Edit mode
            if (id != null && id != 0)
            {
                objPreferenceList = _unitOfWork.PreferenceList.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceList == null)
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
            if (objPreferenceList.Id == 0)
            {
                objPreferenceList.InstructorId = 1; //TODO: this will need to be replaced by a hidden field in the form which captures the instructor id.
                _unitOfWork.PreferenceList.Add(objPreferenceList);
                TempData["success"] = "Preference List added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PreferenceList.Update(objPreferenceList);
                TempData["success"] = "Preference List updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceLists
{
    public class DeleteModelOLD : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceList objPreferenceList { get; set; }
        public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }

        public DeleteModelOLD(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceList = new PreferenceList();
            SemesterInstanceList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            SemesterInstanceList = _unitOfWork.SemesterInstance.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.SemesterInstanceName,
                                Value = c.Id.ToString()
                            });

            if (id != null && id != 0)
            {
                objPreferenceList = _unitOfWork.PreferenceList.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceList == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objPreferenceList = _unitOfWork.PreferenceList.GetById(id);
            if (objPreferenceList == null)
            {
                return NotFound();
            }
            _unitOfWork.PreferenceList.Delete(objPreferenceList);
            TempData["success"] = "Preference List Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }

    }
}

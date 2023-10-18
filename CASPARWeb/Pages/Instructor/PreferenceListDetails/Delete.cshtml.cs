using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetails
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetail objPreferenceListDetail { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetail = new PreferenceListDetail();
            CourseList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = _unitOfWork.Course.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.CourseTitle,
                                Value = c.Id.ToString()
                            });

            if (id != null && id != 0)
            {
                objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceListDetail == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(id);
            if (objPreferenceListDetail == null)
            {
                return NotFound();
            }
            _unitOfWork.PreferenceListDetail.Delete(objPreferenceListDetail);
            TempData["success"] = "Preference List Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objPreferenceListDetail.PreferenceListId });
        }
    }
}

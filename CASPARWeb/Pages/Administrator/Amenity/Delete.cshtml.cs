using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Amenity
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public ClassroomAmenity objClassroomAmenity { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objClassroomAmenity = new ClassroomAmenity();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objClassroomAmenity = _unitOfWork.ClassroomAmenity.GetById(id);
            }
            //Nothing found in DB
            if (objClassroomAmenity == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objClassroomAmenity = _unitOfWork.ClassroomAmenity.GetById(id);
            if (objClassroomAmenity == null)
            {
                return NotFound();
            }
            _unitOfWork.ClassroomAmenity.Delete(objClassroomAmenity);
            TempData["success"] = "Classroom Amenity Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

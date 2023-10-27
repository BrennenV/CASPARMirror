using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Amenity
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public ClassroomAmenity objClassroomAmenity { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objClassroomAmenity = new ClassroomAmenity();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objClassroomAmenity = _unitOfWork.ClassroomAmenity.GetById(id);
            }
            //Nothing found in DB
            if (objClassroomAmenity == null)
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
            if (objClassroomAmenity.Id == 0)
            {
                _unitOfWork.ClassroomAmenity.Add(objClassroomAmenity);
                TempData["success"] = "Classroom Amenity added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.ClassroomAmenity.Update(objClassroomAmenity);
                TempData["success"] = "Classroom Amenity updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Amenity
{
    public class CreateAmenityModel : PageModel
    {
        public int? id;
        public string? amenityName { get; set; }
        public ClassroomAmenity? amenity;
        [BindProperty]
        public string inputAmenityName { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateAmenityModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }


        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the Amenity
                this.id = id;
                amenity = _unitOfWork.ClassroomAmenity.GetById(this.id);
            }
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the Amenity
                //delete the the current Amenity
                ClassroomAmenity tempAmenity = _unitOfWork.ClassroomAmenity.GetById(id);
                _unitOfWork.ClassroomAmenity.Delete(tempAmenity);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new Amenity
            ClassroomAmenity amenity = new ClassroomAmenity();
            amenity.ClassroomAmenityName = inputAmenityName;
            _unitOfWork.ClassroomAmenity.Add(amenity);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

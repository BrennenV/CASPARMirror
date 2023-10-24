using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Campuses
{
    public class CreateCampusModel : PageModel
    {
        public int? id;
        public string? campusName { get; set; }
        public Campus? campus;
        [BindProperty]
        public string inputCampusName { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateCampusModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }


        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the campus
                this.id = id;
                campus = _unitOfWork.Campus.GetById(this.id);
            }
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the campus
                //delete the the current campus
                Campus tempCampus = _unitOfWork.Campus.GetById(id);
                _unitOfWork.Campus.Delete(tempCampus);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new campus
            Campus campus = new Campus();
            campus.CampusName = inputCampusName;
            _unitOfWork.Campus.Add(campus);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}


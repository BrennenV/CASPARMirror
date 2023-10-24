using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Buildings
{
    public class CreateBuildingModel : PageModel
    {
        public int? id;
        public string? buildingName { get; set; }
        public Building building;
        [BindProperty]
        public string inputBuildingName { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }
        [BindProperty]
        public string inputCampus { get; set; }
        public IEnumerable<Campus> DropdownItems { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateBuildingModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public string? tempCampusName { get; set; }
        [BindProperty]
        public string? editing { get; set; }
        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the campus
                editing = "true";
                this.id = id;
                this.building = _unitOfWork.Building.GetById(this.id);
                tempCampusName = _unitOfWork.Campus.Get(c => c.Id == building.CampusId).CampusName;
            }
            DropdownItems = _unitOfWork.Campus.GetAll(null, null, null);
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the building
                //delete the the current building
                Building tempbuilding = _unitOfWork.Building.GetById(id);
                _unitOfWork.Building.Delete(tempbuilding);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new building
            Building building = new Building();
            building.BuildingName = inputBuildingName;
            building.CampusId = _unitOfWork.Campus.Get(c => c.CampusName == inputCampus).Id;
            _unitOfWork.Building.Add(building);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Classrooms
{
    public class CreateClassroomModel : PageModel {
        public int? id;
        public string classroomNumber { get; set; }
        public Classroom classRoom;
        public Building building;
        [BindProperty]
        public string inputClassroomNumber { get; set; }
        [BindProperty]
        public int inputClassroomSeats { get;set; }
        [BindProperty]
        public string inputClassroomDetails { get; set; }
        [BindProperty]
        public string inputBuildingName { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }
        [BindProperty]
        public string inputCampus { get; set; }
        [BindProperty]
        public string inputBuilding {  get; set; }
        public IEnumerable<Campus> DropdownCampus { get; set; }
        public IEnumerable<Building> DropdownBuilding { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateClassroomModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public string? tempCampusName { get; set; }
        [BindProperty]
        public string? tempBuildingName { get; set; }
        [BindProperty]
        public string? editing { get; set; }
        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the classroom
                editing = "true";
                this.id = id;
                this.classRoom = _unitOfWork.Classroom.GetById(this.id);
                building = _unitOfWork.Building.Get(b => b.Id == classRoom.BuildingId);
                tempBuildingName = building.BuildingName;
                tempCampusName = _unitOfWork.Campus.Get(c => c.Id == building.CampusId).CampusName;
            }
            DropdownBuilding = _unitOfWork.Building.GetAll(null, null, null);
            DropdownCampus = _unitOfWork.Campus.GetAll(null, null, null);
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the classroom
                //delete the the current classroom
                Classroom tempClassroom = _unitOfWork.Classroom.GetById(id);
                _unitOfWork.Classroom.Delete(tempClassroom);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new classroom
            Classroom Classroom = new Classroom();
            Classroom.ClassroomNumber = inputClassroomNumber;
            Classroom.ClassroomSeats = inputClassroomSeats;
            Classroom.ClassroomDetail = inputClassroomDetails;
            Classroom.BuildingId = _unitOfWork.Building.Get(b => b.BuildingName == inputBuildingName).Id;
            //set the classroom's amenity

            //save changes
            _unitOfWork.Classroom.Add(Classroom);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

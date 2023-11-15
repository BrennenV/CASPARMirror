using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.Classrooms
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Classroom objClassroom { get; set; }
        public IEnumerable<SelectListItem> BuildingList { get; set; }

        public IEnumerable<ClassroomAmenity> AmenityList { get; set; }
        public IEnumerable<ClassroomAmenityPossession> AmenityPossessionList { get; set; }
        public List<String> AttachedAmenityList { get; set; }
        public List<String> UnattachedAmenityList { get; set; }
        [BindProperty]
        public String returnedAttachedAmenity { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objClassroom = new Classroom();
            BuildingList = new List<SelectListItem>();
            AttachedAmenityList = new List<String>();
            UnattachedAmenityList = new List<String>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            BuildingList = _unitOfWork.Building.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.BuildingName,
                                Value = c.Id.ToString()
                            });

            AmenityPossessionList = _unitOfWork.ClassroomAmenityPossession.GetAll(c => c.ClassroomId == id && c.IsArchived != true);
            foreach (ClassroomAmenityPossession amenityPossession in AmenityPossessionList) {
                //Grab each attached amenity
                String temp = _unitOfWork.ClassroomAmenity.Get(c => c.Id == amenityPossession.ClassroomAmenityId && c.IsArchived != true).ClassroomAmenityName;
                AttachedAmenityList.Add(temp);
            }
            //Fill UnattachedAmenityList will all amenity Names
            AmenityList = _unitOfWork.ClassroomAmenity.GetAll(c => c.IsArchived != true);
            foreach (ClassroomAmenity amenity in AmenityList) {
                UnattachedAmenityList.Add(amenity.ClassroomAmenityName);
            }
            //Remove amenities from UnattachedAmenityList
            //if they are already stored in the AttachedAmenityList
            for (int i = 0; i < AttachedAmenityList.Count(); i++) {
                int l = 0;
                String attachedAmenity = AttachedAmenityList[i];
                while (l < UnattachedAmenityList.Count()) {
                    if (attachedAmenity == UnattachedAmenityList[l]) {
                        UnattachedAmenityList.RemoveAt(l);
                        break;
                    }
                    l++;
                }
            }
            //Edit mode
            if (id != null && id != 0)
            {
                objClassroom = _unitOfWork.Classroom.GetById(id);
            }
            //Nothing found in DB
            if (objClassroom == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            //putting checked amenities into a list
            String[] checkedAmenityList = returnedAttachedAmenity.Split(',');
            //Creating a Row
            if (objClassroom.Id == 0)
            {
                _unitOfWork.Classroom.Add(objClassroom);
                _unitOfWork.Commit();
                int classroomId = _unitOfWork.Classroom.Get(c => c == objClassroom && c.IsArchived != true).Id;
                //add the checked amenities to the classroom
                foreach (String s in checkedAmenityList) {
                    ClassroomAmenityPossession temp = new ClassroomAmenityPossession();
                    temp.ClassroomId = classroomId;
                    temp.ClassroomAmenityId = _unitOfWork.ClassroomAmenity.Get(c => c.ClassroomAmenityName == s && c.IsArchived != true).Id;

                    _unitOfWork.ClassroomAmenityPossession.Add(temp);
                    _unitOfWork.Commit();
                }
                TempData["success"] = "Classroom added Successfully";
            }
            //Modifying a Row
            else
            {
                //arcive all attached ClassroomAmenityPossession's
                IEnumerable<ClassroomAmenityPossession> tempList = _unitOfWork.ClassroomAmenityPossession.GetAll(c => c.ClassroomId == objClassroom.Id && c.IsArchived != true);
                foreach (ClassroomAmenityPossession ap in tempList) {
                    ap.IsArchived = true;
                    _unitOfWork.ClassroomAmenityPossession.Update(ap);
                }
                //add the checked amenities to the classroom
                foreach (String s in checkedAmenityList) {
                    ClassroomAmenityPossession temp = new ClassroomAmenityPossession();
                    temp.ClassroomId = objClassroom.Id;
                    temp.ClassroomAmenityId = _unitOfWork.ClassroomAmenity.Get(c => c.ClassroomAmenityName == s && c.IsArchived != true).Id;

                    _unitOfWork.ClassroomAmenityPossession.Add(temp);
                }
                _unitOfWork.Classroom.Update(objClassroom);
                TempData["success"] = "Classroom updated Successfully";
            }

            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

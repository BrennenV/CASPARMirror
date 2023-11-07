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
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objClassroom = new Classroom();
            BuildingList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            BuildingList = _unitOfWork.Building.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.BuildingName,
                                Value = c.Id.ToString()
                            });
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
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            //Creating a Row
            if (objClassroom.Id == 0)
            {
                _unitOfWork.Classroom.Add(objClassroom);
                TempData["success"] = "Classroom added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.Classroom.Update(objClassroom);
                TempData["success"] = "Classroom updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

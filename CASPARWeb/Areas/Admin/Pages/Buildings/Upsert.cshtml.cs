using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Admin.Pages.Buildings
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Building objBuilding { get; set; }
        public IEnumerable<SelectListItem> CampusList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objBuilding = new Building();
            CampusList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true)
                            .Select(c => new SelectListItem
                            {
                                Text = c.CampusName,
                                Value = c.Id.ToString()
                            });
            //Edit mode
            if (id != null && id != 0)
            {
                objBuilding = _unitOfWork.Building.GetById(id);
            }
            //Nothing found in DB
            if (objBuilding == null)
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
            if (objBuilding.Id == 0)
            {
                _unitOfWork.Building.Add(objBuilding);
                TempData["success"] = "Building added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.Building.Update(objBuilding);
                TempData["success"] = "Building updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

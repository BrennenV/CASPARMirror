using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.Buildings
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Building objBuilding { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objBuilding = new Building();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objBuilding = _unitOfWork.Building.GetById(id);
            }
            //Nothing found in DB
            if (objBuilding == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objBuilding = _unitOfWork.Building.GetById(id);
            if (objBuilding == null)
            {
                return NotFound();
            }
            _unitOfWork.Building.Delete(objBuilding);
            TempData["success"] = "Building Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

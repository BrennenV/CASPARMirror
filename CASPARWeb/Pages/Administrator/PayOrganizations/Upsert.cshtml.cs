using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.PayOrganizations
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PayOrganization objPayOrganization { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPayOrganization = new PayOrganization();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objPayOrganization = _unitOfWork.PayOrganization.GetById(id);
            }
            //Nothing found in DB
            if (objPayOrganization == null)
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
            if (objPayOrganization.Id == 0)
            {
                _unitOfWork.PayOrganization.Add(objPayOrganization);
                TempData["success"] = "Pay Organization added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PayOrganization.Update(objPayOrganization);
                TempData["success"] = "Pay Organization updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

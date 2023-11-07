using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.PayOrganizations
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PayOrganization objPayOrganization { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPayOrganization = new PayOrganization();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objPayOrganization = _unitOfWork.PayOrganization.GetById(id);
            }
            //Nothing found in DB
            if (objPayOrganization == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objPayOrganization = _unitOfWork.PayOrganization.GetById(id);
            if (objPayOrganization == null)
            {
                return NotFound();
            }
            _unitOfWork.PayOrganization.Delete(objPayOrganization);
            TempData["success"] = "Pay Organization Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

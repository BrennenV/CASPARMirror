using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.PayModels
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PayModel objPayModel { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPayModel = new PayModel();
        }
        public IActionResult OnGet(int? id)
        {

            if (id != null && id != 0)
            {
                objPayModel = _unitOfWork.PayModel.GetById(id);
            }
            //Nothing found in DB
            if (objPayModel == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            objPayModel = _unitOfWork.PayModel.GetById(id);
            if (objPayModel == null)
            {
                return NotFound();
            }
            _unitOfWork.PayModel.Delete(objPayModel);
            TempData["success"] = "Pay Model Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

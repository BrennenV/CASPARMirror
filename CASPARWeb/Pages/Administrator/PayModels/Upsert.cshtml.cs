using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.PayModels
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PayModel objPayModel { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPayModel = new PayModel();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objPayModel = _unitOfWork.PayModel.GetById(id);
            }
            //Nothing found in DB
            if (objPayModel == null)
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
            if (objPayModel.Id == 0)
            {
                _unitOfWork.PayModel.Add(objPayModel);
                TempData["success"] = "Pay Model added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PayModel.Update(objPayModel);
                TempData["success"] = "Pay Model updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

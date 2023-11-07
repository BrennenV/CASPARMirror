using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.PartOfDays
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PartOfDay objPartOfDay { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPartOfDay = new PartOfDay();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objPartOfDay = _unitOfWork.PartOfDay.GetById(id);
            }
            //Nothing found in DB
            if (objPartOfDay == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objPartOfDay = _unitOfWork.PartOfDay.GetById(id);
            if (objPartOfDay == null)
            {
                return NotFound();
            }
            _unitOfWork.PartOfDay.Delete(objPartOfDay);
            TempData["success"] = "Time Of Day Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

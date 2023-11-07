using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.TimeBlocks
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public TimeBlock objTimeBlock { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objTimeBlock = new TimeBlock();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objTimeBlock = _unitOfWork.TimeBlock.GetById(id);
            }
            //Nothing found in DB
            if (objTimeBlock == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objTimeBlock = _unitOfWork.TimeBlock.GetById(id);
            if (objTimeBlock == null)
            {
                return NotFound();
            }
            _unitOfWork.TimeBlock.Delete(objTimeBlock);
            TempData["success"] = "Time Block Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

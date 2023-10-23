using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public User objUser { get; set; }
        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objUser = new User();
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objUser = _unitOfWork.User.GetById(id);
            }
            //Nothing found in DB
            if (objUser == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objUser = _unitOfWork.User.GetById(id);
            if (objUser == null)
            {
                return NotFound();
            }
            _unitOfWork.User.Delete(objUser);
            TempData["success"] = "User Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.Users
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public User objUser { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objUser = new User();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objUser = _unitOfWork.User.GetById(id);
            }
            //Nothing found in DB
            if (objUser == null)
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
            if (objUser.Id == 0)
            {
                _unitOfWork.User.Add(objUser);
                TempData["success"] = "User added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.User.Update(objUser);
                TempData["success"] = "User updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

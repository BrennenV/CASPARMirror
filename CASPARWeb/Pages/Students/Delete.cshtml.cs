using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public WishlistDetailModality objWDM { get; set; }

        public DeleteModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            objWDM = new WishlistDetailModality();

            objWDM = _unitOfWork.WishlistDetailModality.GetById(id);

            if (objWDM == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var objWDM = _unitOfWork.WishlistDetailModality.GetById(id);
            if (objWDM == null)
            {
                return NotFound();
            }

            _unitOfWork.WishlistDetailModality.Delete(objWDM);
            TempData["success"] = "WishlistDetailModality Deleted Successfully";
            _unitOfWork.Commit();

            return RedirectToPage("./Index");
        }
    }
}

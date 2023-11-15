using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CASPARWeb.Areas.Instr.Pages.Wishlists
{
    public class IndexModel : PageModel
    {
        public int SelectedSemesterId;
		[BindProperty]
		public int WishlistId { get; set; }
		public IEnumerable<WishlistCourse> objCourseList;
		public IEnumerable<SemesterInstance> objSemesterInstanceList;
		public IEnumerable<SelectListItem> CourseWishlistList { get; set; }
		private readonly UnitOfWork _unitOfWork;
		public IndexModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
            CourseWishlistList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int? selectedSemesterId)
        {
            //This code is to help preserve the selected semester id upon returning to the index page
            if (selectedSemesterId != null)
            {
                SelectedSemesterId = (int)selectedSemesterId;
            }
            else
            {
				DateTime currentDate = DateTime.Now;
				SemesterInstance semesterInstance = _unitOfWork.SemesterInstance.GetAll(s => s.StartDate > currentDate).OrderBy(s => s.StartDate).FirstOrDefault();
				SelectedSemesterId = semesterInstance.Id;
			}

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			Wishlist wishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);

			if (wishlist == null)
			{
				wishlist = new Wishlist
				{
					SemesterInstanceId = SelectedSemesterId,
					UserId = userId
				};

				_unitOfWork.Wishlist.Add(wishlist);
				_unitOfWork.Commit();
			}

			objCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlist.Id, null, "Wishlist,Course,Course.AcademicProgram");

			return Page();
        }

		public IActionResult OnGetTableData(int selectedSemesterId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Wishlist wishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == selectedSemesterId && w.UserId == userId);

			if (wishlist == null)
			{
				wishlist = new Wishlist
				{
					SemesterInstanceId = selectedSemesterId,
					UserId = userId
				};

				_unitOfWork.Wishlist.Add(wishlist);
				_unitOfWork.Commit();
			}

			var objCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlist.Id, null, "Wishlist,Course,Course.AcademicProgram");

			// Convert objCourseList to the format you need and return it
			// If you want to return HTML, you can create a partial view and return it like this:
			return Partial("_TableData", objCourseList);
			// If you want to return JSON, you can do it like this:
			// return Json(objCourseList);
		}

	}
}

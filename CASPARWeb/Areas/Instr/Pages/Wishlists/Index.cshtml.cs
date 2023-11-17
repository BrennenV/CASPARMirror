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
		[BindProperty]
		public int SelectedSemesterId { get; set; }
		[BindProperty]
		public int WishlistId { get; set; }
		[BindProperty]
		public int Rank { get; set; }
		[BindProperty]
		public string SelectedCourse { get; set; }
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

		public IActionResult OnPostAdd(int? selectedSemesterId, int selectedCourse)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			SelectedSemesterId = (int)selectedSemesterId;

			// get the wishlistId for the current user and semester
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Wishlist wishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);
			// add the course to the wishlistCourse where the wishlistId matches the wishlistId for the current user and semester
			WishlistCourse wishlistCourse = new WishlistCourse
			{
				WishlistId = wishlist.Id,
				CourseId = selectedCourse,
				PreferenceRank = Rank
			};

			_unitOfWork.WishlistCourse.Add(wishlistCourse);
			_unitOfWork.Commit();


			return RedirectToPage("./Index");
		}

	}
}

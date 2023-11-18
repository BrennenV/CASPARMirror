using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishlistCourseController : Controller
	{
		private readonly UnitOfWork _unitOfWork;
		public WishlistCourseController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Json(new { data = _unitOfWork.WishlistCourse.GetAll(c => c.IsArchived != true, null, "Wishlist,Course,Course.AcademicProgram") });
		}

		[HttpPost]
		public IActionResult OnPostArchiveCourse(int? selectedCourse)
		{
			_unitOfWork.WishlistCourse.Delete(_unitOfWork.WishlistCourse.Get(w => w.Id == (int)selectedCourse));
			_unitOfWork.Commit();

			return RedirectToPage("./Index");
		}
	}
}

using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PreferenceDetailController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		public PreferenceDetailController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Get()
		{
			//TODO: this will need to return the details for the logged in instructor.
			//THIS CONTROLLER WILL LIKELY NEED TO BE DELETED
			return Json(new { data = _unitOfWork.Wishlist.GetAll(null, null, "Wiishlist,WishlistModality,Modality,WishlistTimeBlock,TimeBlock,WishlistDaysOfWeek,DaysOfWeek,WishListCourse,Course,WishlistCampus,Campus,WishList.SemesterInstance,SemesterInstance,WishListCourse.Course.AcedemicProgram") });
		}
	}
}

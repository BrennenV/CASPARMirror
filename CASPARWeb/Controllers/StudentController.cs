using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		public StudentController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Json(new { data = _unitOfWork.WishlistDetailModality.GetAll(null, null, "WishlistDetail,Modality,TimeOfDay,WishlistDetail.Wishlist,WishlistDetail.Course,Campus,WishlistDetail.Wishlist.SemesterInstance,WishlistDetail.Course.AcademicProgram") });
		}
	}
}

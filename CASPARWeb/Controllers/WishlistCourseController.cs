﻿using DataAccess;
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
			//THIS CONTROLLER WILL LIKELY NEED TO BE DELETED
			return Json(new { data = _unitOfWork.WishlistCourse.GetAll(null, null, "Wishlist,Course,Course.AcademicProgram") });
		}
	}
}

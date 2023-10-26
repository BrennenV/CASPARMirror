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
			return Json(new { data = _unitOfWork.PreferenceListDetailModality.GetAll(c => c.PreferenceListDetail.PreferenceList.InstructorId == 1, null, "PreferenceListDetail,Modality,TimeBlock,DaysOfWeek,PreferenceListDetail.PreferenceList,PreferenceListDetail.Course,Campus,PreferenceListDetail.PreferenceList.SemesterInstance,PreferenceListDetail.Course.AcademicProgram") });
		}
	}
}

using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceListController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PreferenceListController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //TODO: this will eventually need to get only lists from the currently logged in instructor
            return Json(new { data = _unitOfWork.PreferenceList.GetAll(c => c.InstructorId == 1, null, "Instructor,SemesterInstance") });
        }
    }
}

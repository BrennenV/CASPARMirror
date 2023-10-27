using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller   
    {
        private readonly UnitOfWork _unitOfWork;
        public CourseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Course.GetAll(null, null, "AcademicProgram") });
        }
    }
}

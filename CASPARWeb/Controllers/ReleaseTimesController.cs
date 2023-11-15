using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseTimesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public ReleaseTimesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(int? id)
        {
            return Json(new { data = _unitOfWork.ReleaseTime.GetAll(c => c.IsArchived != true, null, "Semester,Instructor") });
        }
    }
}

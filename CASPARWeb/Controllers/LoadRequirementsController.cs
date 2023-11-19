using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadRequirementsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public LoadRequirementsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.SemesterInstance.GetAll(c => c.IsArchived != true, null) });
        }
    }
}

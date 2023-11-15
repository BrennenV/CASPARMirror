using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterInstanceController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public SemesterInstanceController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.SemesterInstance.GetAll(c => c.IsArchived != true, null) });
        }
    }
}

using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicProgramListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public AcademicProgramListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.AcademicProgram.GetAll(c => c.IsArchived != true, null) });
        }
    }
}

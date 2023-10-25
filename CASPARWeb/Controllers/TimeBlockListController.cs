using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TimeBlockListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public TimeBlockListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.TimeBlock.GetAll(null, null) });
        }
    }
}

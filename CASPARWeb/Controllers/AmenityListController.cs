using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public AmenityListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.ClassroomAmenity.GetAll(null, null) });
        }
    }
}

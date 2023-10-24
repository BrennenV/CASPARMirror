using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public BuildingListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.Building.GetAll(null, null, "Campus") });
        }
    }
}


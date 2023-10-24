using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CampusListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public CampusListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.Campus.GetAll(null, null) });
        }
    }
}


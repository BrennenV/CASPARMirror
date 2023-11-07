using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PartOfDayListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public PartOfDayListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.PartOfDay.GetAll(null, null) });
        }
    }
}

using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TimeOfDayListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public TimeOfDayListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.TimeOfDay.GetAll(null, null) });
        }
    }
}

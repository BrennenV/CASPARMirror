using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public UserController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.User.GetAll(null, null) });
        }
    }
}

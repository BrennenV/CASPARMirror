using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartOfTermController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PartOfTermController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.PartOfTerm.GetAll(c => c.IsArchived != true) });
        }
    }
}

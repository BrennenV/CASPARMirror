using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOrganizationController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PayOrganizationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.PayOrganization.GetAll(c => c.IsArchived != true) });
        }
    }
}

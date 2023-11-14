using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceListDetailModalityController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PreferenceListDetailModalityController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(int? id)
        {
            //TODO: this will eventually need to get only details from the currently logged in instructor
            //THIS CONTROLLER WILL LIKELY NEED TO BE DELETED
            return Json(new { data = _unitOfWork.Wishlist.GetAll(c => c.IsArchived != true, null, "Modality,TimeBlock,DaysOfWeek,Campus") });
        }
    }
}

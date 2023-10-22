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
            return Json(new { data = _unitOfWork.PreferenceListDetailModality.GetAll(c => c.PreferenceListDetailId == id, null, "Modality,TimeBlock,DaysOfWeek,Campus") });
        }
    }
}

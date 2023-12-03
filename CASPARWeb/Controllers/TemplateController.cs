using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public TemplateController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(int? id)
        {
            int semesterId = _unitOfWork.SemesterInstance.GetById(id).SemesterId;
            Expression<Func<Template, bool>> predicate = c => c.SemesterId == semesterId && c.IsArchived != true;
            return Json(new { data = _unitOfWork.Template.GetAll(predicate, null, "Course,Semester,Course.AcademicProgram") });
        }
    }
}

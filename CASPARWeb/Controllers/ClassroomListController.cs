using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace CASPARWeb.Controllers {
    public class test : Controller {
        public Classroom classroom { get; set; }
        public List<ClassroomAmenity> classroomsList { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomListController : Controller {
        private readonly UnitOfWork _unitOfWork;
        public ClassroomListController(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.Classroom.GetAll(null, null, "Building,Building.Campus") });//"Building,Building.Campus,ClassroomAmenityPossession.ClassroomAmenity"
            //data = _unitOfWork.Classroom.GetAll(null, null, "Building,Building.Campus")
        }
    }
}

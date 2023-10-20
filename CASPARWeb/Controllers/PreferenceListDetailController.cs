﻿using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceListDetailController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PreferenceListDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(int? id)
        {
            //TODO: this will eventually need to get only details from the currently logged in instructor
            return Json(new { data = _unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceListId == id, null, "Course,PreferenceList") });
        }
    }
}

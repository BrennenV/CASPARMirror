using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASPARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceListController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public PreferenceListController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //TODO: this will eventually need to get only lists from the currently logged in instructor
            //List<PreferenceList> preferenceLists = _unitOfWork.PreferenceList.GetAll(c => c.InstructorId == 1, null, "Instructor,SemesterInstance").ToList();
            //List<PreferenceListDetail> preferenceListDetails = _unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1, null, "PreferenceList,Course").ToList();

            //Dictionary<int, string> detailsDictionary = new Dictionary<int, string>();

            //foreach(PreferenceList preferenceList in preferenceLists)
            //{
            //    int plistId = preferenceList.Id;
            //    string detailsString = "";
            //    bool isFirst = true;
            //    foreach (PreferenceListDetail preferenceListDetail in preferenceListDetails)
            //    {
            //        if(preferenceListDetail.PreferenceListId == plistId)
            //        {
            //            if(isFirst)
            //            {
            //                isFirst = false;
            //                detailsString += preferenceListDetail.Course.CourseTitle;
            //            }
            //            else
            //            {
            //                detailsString += ", " + preferenceListDetail.Course.CourseTitle;
            //            }
            //        }
            //    }

            //    detailsDictionary[plistId] = detailsString;
            //}

            return Json(new { data = _unitOfWork.PreferenceList.GetAll(c => c.InstructorId == 1, null, "Instructor,SemesterInstance") });
        }
    }
}

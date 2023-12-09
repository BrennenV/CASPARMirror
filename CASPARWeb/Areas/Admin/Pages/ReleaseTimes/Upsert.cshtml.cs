using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.ReleaseTimes
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
        [BindProperty]
        public IEnumerable<ReleaseTime> objReleaseTimeList { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemesterInstance = new SemesterInstance();
            objReleaseTimeList = new List<ReleaseTime>();
        }
        public IActionResult OnGet(int? id)
        {
            Expression<Func<SemesterInstance, bool>> predicate = c => c.Id == id && c.IsArchived != true;
            objSemesterInstance = _unitOfWork.SemesterInstance.Get(predicate, true, "Semester");
            objReleaseTimeList = _unitOfWork.ReleaseTime.GetAll(c => c.SemesterInstanceId == id && c.IsArchived != true, null, "ApplicationUser").OrderBy(c => c.ApplicationUser.LastName);
            return Page();
        }
        public IActionResult OnPost()
        {
            objSemesterInstance = _unitOfWork.SemesterInstance.Get(c => c.Id == objReleaseTimeList.First().SemesterInstanceId);
            foreach (var objReleaseTime in objReleaseTimeList)
            {
                objReleaseTime.IsArchived = false;
                //Creating a Row (This probably will never be used)
                if (objReleaseTime.Id == 0)
                {
                    _unitOfWork.ReleaseTime.Add(objReleaseTime);
                    TempData["success"] = "Release Time added Successfully";
                }
                //Modifying a Row
                else
                {
                    _unitOfWork.ReleaseTime.Update(objReleaseTime);
                    TempData["success"] = "Release Times updated Successfully";
                }
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Upsert", new { id = objReleaseTimeList.First().SemesterInstanceId });
        }
    }
}

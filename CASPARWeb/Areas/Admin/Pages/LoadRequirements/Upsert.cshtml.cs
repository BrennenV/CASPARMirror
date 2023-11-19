using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.LoadRequirements
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
        [BindProperty]
        public IEnumerable<LoadReq> objLoadReqList { get; set; }
        public IEnumerable<SelectListItem> InstructorList { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemesterInstance = new SemesterInstance();
            objLoadReqList = new List<LoadReq>();
            InstructorList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            InstructorList = _unitOfWork.ApplicationUser.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.FirstName,
                                Value = c.Id.ToString()
                            });
            objSemesterInstance = _unitOfWork.SemesterInstance.Get(c => c.Id == id && c.IsArchived != true, true);
            objLoadReqList = _unitOfWork.LoadReq.GetAll(c => c.SemesterInstanceId == id && c.IsArchived != true, null, "ApplicationUser,SemesterInstance");
            return Page();
        }
        public void OnPost()
        {
            objSemesterInstance = _unitOfWork.SemesterInstance.Get(c => c.Id == objLoadReqList.First().SemesterInstanceId);
            foreach (var objLoadReq in objLoadReqList)
            {
                objLoadReq.IsArchived = false;
                objLoadReq.SemesterInstance = _unitOfWork.SemesterInstance.Get(c => c.Id == objLoadReq.SemesterInstanceId);
                objLoadReq.ApplicationUser = _unitOfWork.ApplicationUser.Get(c => c.Id == objLoadReq.InstructorId);
                //Creating a Row
                if (objLoadReq.Id == 0)
                {
                    _unitOfWork.LoadReq.Add(objLoadReq);
                    TempData["success"] = "Load Requirement added Successfully";
                }
                //Modifying a Row
                else
                {
                    _unitOfWork.LoadReq.Update(objLoadReq);
                    TempData["success"] = "Load Requirement updated Successfully";
                }
            }
            //Saves changes
            _unitOfWork.Commit();
            //return RedirectToPage("./Index", new { id = objTemplate.SemesterId });
        }
    }
}

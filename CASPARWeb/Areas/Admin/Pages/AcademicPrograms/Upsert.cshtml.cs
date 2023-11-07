using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Admin.Pages.AcademicPrograms
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public AcademicProgram objAcademicProgram { get; set; }
        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objAcademicProgram = new AcademicProgram();
        }
        public IActionResult OnGet(int? id)
        {
            //Edit mode
            if (id != null && id != 0)
            {
                objAcademicProgram = _unitOfWork.AcademicProgram.GetById(id);
            }
            //Nothing found in DB
            if (objAcademicProgram == null)
            {
                return NotFound();
            }
            //Create mode
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }
            //Creating a Row
            if (objAcademicProgram.Id == 0)
            {
                _unitOfWork.AcademicProgram.Add(objAcademicProgram);
                TempData["success"] = "Academic Program added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.AcademicProgram.Update(objAcademicProgram);
                TempData["success"] = "Academic Program updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.AcademicPrograms
{
    public class CreateAcademicProgramModel : PageModel
    {
        public int? id;
        public string? programName { get; set; }
        public AcademicProgram? academicProgram;
        [BindProperty]
        public string inputProgramName { get; set; }
        [BindProperty]
        public string inputProgramCode { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateAcademicProgramModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }


        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the campus
                this.id = id;
                academicProgram = _unitOfWork.AcademicProgram.GetById(this.id);
            }
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the campus
                //delete the the current campus
                AcademicProgram tempAcademicProgram = _unitOfWork.AcademicProgram.GetById(id);
                _unitOfWork.AcademicProgram.Delete(tempAcademicProgram);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new campus
            AcademicProgram academicProgram = new AcademicProgram();
            academicProgram.ProgramName = inputProgramName;
            academicProgram.ProgramCode = inputProgramCode;
            _unitOfWork.AcademicProgram.Add(academicProgram);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

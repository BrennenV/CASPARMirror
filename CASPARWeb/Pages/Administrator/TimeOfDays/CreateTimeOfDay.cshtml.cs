using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.TimeOfDays
{
    public class CreateTimeOfDayModel : PageModel
    {
        public int? id;
        public string? partOfDay { get; set; }
        public TimeOfDay? timeOfDay;
        [BindProperty]
        public string inputTimeOfDay { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateTimeOfDayModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }


        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the campus
                this.id = id;
                timeOfDay = _unitOfWork.TimeOfDay.GetById(this.id);
            }
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the campus
                //delete the the current campus
                TimeOfDay tempTimeOfDay = _unitOfWork.TimeOfDay.GetById(id);
                _unitOfWork.TimeOfDay.Delete(tempTimeOfDay);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new campus
            TimeOfDay timeOfDay = new TimeOfDay();
            timeOfDay.PartOfDay = inputTimeOfDay;
            _unitOfWork.TimeOfDay.Add(timeOfDay);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

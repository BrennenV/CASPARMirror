using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Administrator.TimeBlocks
{
    public class CreateTimeBlockModel : PageModel
    {
        public int? id;
        public string? timeBlockValue { get; set; }
        public TimeBlock? timeBlock;
        [BindProperty]
        public string inputTimeBlockValue { get; set; }
        [BindProperty]
        public string inputPressedDelete { get; set; }

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public CreateTimeBlockModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }


        public void OnGet(int? id) {//occures when the page first loads
            if (id != null) {
                //if your editing the campus
                this.id = id;
                timeBlock = _unitOfWork.TimeBlock.GetById(this.id);
            }
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"
            if (id != null) {
                //you are editing the campus
                //delete the the current campus
                TimeBlock tempTimeBlock = _unitOfWork.TimeBlock.GetById(id);
                _unitOfWork.TimeBlock.Delete(tempTimeBlock);
                _unitOfWork.Commit();
                if (inputPressedDelete == "true") {//pressed the delete button
                    return RedirectToPage("./Index");
                }
            }

            //create the new campus
            TimeBlock timeBlock = new TimeBlock();
            timeBlock.TimeBlockValue = inputTimeBlockValue;
            _unitOfWork.TimeBlock.Add(timeBlock);
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}

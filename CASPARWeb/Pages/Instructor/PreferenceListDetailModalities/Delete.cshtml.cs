using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetailModalities
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetailModality objPreferenceListDetailModality { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
        public IEnumerable<SelectListItem> CampusList { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
        public IEnumerable<SelectListItem> TimeBlockList { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetailModality = new PreferenceListDetailModality();
            ModalityList = new List<SelectListItem>();
            CampusList = new List<SelectListItem>();
            DaysOfWeekList = new List<SelectListItem>();
            TimeBlockList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            ModalityList = _unitOfWork.Modality.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.ModalityName,
                                Value = c.Id.ToString()
                            });
            CampusList = _unitOfWork.Campus.GetAll(null)
                           .Select(c => new SelectListItem
                           {
                               Text = c.CampusName,
                               Value = c.Id.ToString()
                           });
            DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.DaysOfWeekTitle,
                                Value = c.Id.ToString()
                            });
            TimeBlockList = _unitOfWork.TimeBlock.GetAll()
                            .Select(c => new SelectListItem
                            {
                                Text = c.TimeBlockValue,
                                Value = c.Id.ToString()
                            });

            if (id != null && id != 0)
            {
                objPreferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceListDetailModality == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int? id)
        {
            var objPreferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
            if (objPreferenceListDetailModality == null)
            {
                return NotFound();
            }
            _unitOfWork.PreferenceListDetailModality.Delete(objPreferenceListDetailModality);
            TempData["success"] = "Modality Deleted Successfully";
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objPreferenceListDetailModality.PreferenceListDetailId });
        }
    }
}

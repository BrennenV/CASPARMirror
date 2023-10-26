using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetailModalities
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetailModality objPreferenceListDetailModality { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
        public IEnumerable<SelectListItem> CampusList { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
        public IEnumerable<SelectListItem> TimeBlockList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetailModality = new PreferenceListDetailModality();
            ModalityList = new List<SelectListItem>();
            CampusList = new List<SelectListItem>();
            DaysOfWeekList = new List<SelectListItem>();
            TimeBlockList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int? id, int preferenceListDetailId)
        {
            //Capture the PreferenceListDetailId (AKA list item) to use for new details
            if (preferenceListDetailId != 0)
            {
                objPreferenceListDetailModality.PreferenceListDetailId = preferenceListDetailId;
            }
            //Edit mode
            if (id != null && id != 0)
            {
                objPreferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
            }
            //Nothing found in DB
            if (objPreferenceListDetailModality == null)
            {
                return NotFound();
            }

            //Grab all the ids for the options
            ModalityList = _unitOfWork.Modality.GetAll(null)
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
            DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll(null)
                            .Select(c => new SelectListItem
                            {
                                Text = c.DaysOfWeekTitle,
                                Value = c.Id.ToString()
                            });
            TimeBlockList = _unitOfWork.TimeBlock.GetAll(null)
                            .Select(c => new SelectListItem
                            {
                                Text = c.TimeBlockValue,
                                Value = c.Id.ToString()
                            });

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
            if (objPreferenceListDetailModality.Id == 0)
            {
                _unitOfWork.PreferenceListDetailModality.Add(objPreferenceListDetailModality);
                TempData["success"] = "Modality added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.PreferenceListDetailModality.Update(objPreferenceListDetailModality);
                TempData["success"] = "Modality updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objPreferenceListDetailModality.PreferenceListDetailId });
        }
    }
}

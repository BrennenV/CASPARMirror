using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceLists
{
    public class UpdateModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetailModality objPreferenceListDetailModality { get; set; }
		[BindProperty]
		public int CourseId { get; set; }
		[BindProperty]
        public int originalCourseId { get; set; }
		[BindProperty]
        public int currentSemesterInstanceId { get; set; }
		public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
        public IEnumerable<SelectListItem> CampusList { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
        public IEnumerable<SelectListItem> TimeBlockList { get; set; }

        public UpdateModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetailModality = new PreferenceListDetailModality();
            CourseList = new List<SelectListItem>();
            ModalityList = new List<SelectListItem>();
            CampusList = new List<SelectListItem>();
            DaysOfWeekList = new List<SelectListItem>();
            TimeBlockList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int id, int semesterInstanceId)
        {
            objPreferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
            PreferenceListDetail objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(objPreferenceListDetailModality.PreferenceListDetailId);
            CourseId = objPreferenceListDetail.CourseId;
            originalCourseId = CourseId;
            currentSemesterInstanceId = semesterInstanceId;

            //Nothing found in DB
            if (objPreferenceListDetailModality == null)
            {
                return NotFound();
            }

            //Grab all the ids for the options
            CourseList = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == semesterInstanceId, null, "Course,SemesterInstance,Course.AcademicProgram")
                            .Select(c => new SelectListItem
                            {
                                Text = c.Course.AcademicProgram.ProgramCode + " " + c.Course.CourseNumber,
                                Value = c.CourseId.ToString()
                            });
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

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Data Incomplete";
                return Page();
            }

            //Check if they changed the CourseId
            if(originalCourseId == CourseId)
            {
                //Course not changed, just update the Modality
                emptyLocaleAndDateIfOnline();
				_unitOfWork.PreferenceListDetailModality.Update(objPreferenceListDetailModality);
				TempData["success"] = "Modality updated Successfully";
			}
            else
            {
                PreferenceListDetail oldPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(objPreferenceListDetailModality.PreferenceListDetailId);
                //Course changed, reassign modality or perhaps create new PreferenceListDetail and assign the modality to it
                if(_unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1 && c.PreferenceList.SemesterInstanceId == currentSemesterInstanceId && c.CourseId == CourseId, null, "PreferenceList").Count() == 0) //TODO: instructor id needs to be from logged in instructor
                {
                    //The requested course does not yet have a detail already made for it so we add a new detail
                    PreferenceListDetail newPreferenceListDetail = new PreferenceListDetail();
                    newPreferenceListDetail.PreferenceListId = oldPreferenceListDetail.PreferenceListId;
                    newPreferenceListDetail.CourseId = CourseId;

                    //We need to set the Preference Rank now by finding the max highest rank so far
                    IEnumerable<PreferenceListDetail> preferenceListDetails = _unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1 && c.PreferenceList.SemesterInstanceId == currentSemesterInstanceId, null, "PreferenceList");
                    int highestRank = 0;
                    foreach(PreferenceListDetail detail in preferenceListDetails)
                    {
                        if(highestRank < detail.PreferenceRank)
                        {
                            highestRank = detail.PreferenceRank;
                        }
                    }

                    newPreferenceListDetail.PreferenceRank = highestRank + 1;

                    _unitOfWork.PreferenceListDetail.Add(newPreferenceListDetail);
                    _unitOfWork.Commit();

                    //Then assign the modality to the new detail
                    objPreferenceListDetailModality.PreferenceListDetailId = newPreferenceListDetail.Id;
				}
                else
                {
                    //The detail exists so find the detail
                    PreferenceListDetail objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1 && c.PreferenceList.SemesterInstanceId == currentSemesterInstanceId && c.CourseId == CourseId, null, "PreferenceList").First(); //TODO: instructor id needs to be from logged in instructor
                    objPreferenceListDetailModality.PreferenceListDetailId = objPreferenceListDetail.Id;
				}

                emptyLocaleAndDateIfOnline();
				_unitOfWork.PreferenceListDetailModality.Update(objPreferenceListDetailModality);
				TempData["success"] = "Modality updated Successfully";
			}
            
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { selectedSemesterId = currentSemesterInstanceId });
        }

        private void emptyLocaleAndDateIfOnline()
        {
            //If the user chooses online as their modality, it can lead to a case where the dates and location are set on an online modality
            //this ensures that those are cleared before updating the PreferenceListDetailModality
			if (_unitOfWork.Modality.GetById(objPreferenceListDetailModality.ModalityId).ModalityName == "Online")
			{
				objPreferenceListDetailModality.CampusId = null;
				objPreferenceListDetailModality.DaysOfWeekId = null;
				objPreferenceListDetailModality.TimeBlockId = null;
			}
		}
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Coord.Pages.BuildSchedule
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public CourseSection objCourseSection { get; set; }
        public SemesterInstance objSemesterInstance { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
        public IEnumerable<SelectListItem> ClassroomList { get; set; }
        public IEnumerable<SelectListItem> TimeBlockList { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
        public IEnumerable<SelectListItem> PartOfTermList { get; set; }
        public IEnumerable<SelectListItem> PayModelList { get; set; }
        public IEnumerable<SelectListItem> PayOrganizationList { get; set; }
        public IEnumerable<SelectListItem> SectionStatusList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objCourseSection = new CourseSection();
            objSemesterInstance = new SemesterInstance();
            CourseList = new List<SelectListItem>();
            SemesterInstanceList = new List<SelectListItem>();
            ApplicationUserList = new List<SelectListItem>();
            ModalityList = new List<SelectListItem>();
            ClassroomList = new List<SelectListItem>();
            TimeBlockList = new List<SelectListItem>();
            DaysOfWeekList = new List<SelectListItem>();
            PartOfTermList = new List<SelectListItem>();
            PayModelList = new List<SelectListItem>();
            PayOrganizationList = new List<SelectListItem>();
            SectionStatusList = new List<SelectListItem>();
        }
        public IActionResult OnGet(int courseSectionId, int semesterInstanceId)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = _unitOfWork.Course.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.CourseTitle, Value = c.Id.ToString() });
            SemesterInstanceList = _unitOfWork.SemesterInstance.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.SemesterInstanceName, Value = c.Id.ToString() });
            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll().Select(c => new SelectListItem { Text = c.FullName, Value = c.Id.ToString() });
            ModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.ModalityName, Value = c.Id.ToString() });
            ClassroomList = _unitOfWork.Classroom.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.ClassroomNumber, Value = c.Id.ToString() });
            TimeBlockList = _unitOfWork.TimeBlock.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.TimeBlockValue, Value = c.Id.ToString() });
            DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.DaysOfWeekValue, Value = c.Id.ToString() });
            PartOfTermList = _unitOfWork.PartOfTerm.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PartOfTermTitle, Value = c.Id.ToString() });
            PayModelList = _unitOfWork.PayModel.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PayModelTitle, Value = c.Id.ToString() });
            PayOrganizationList = _unitOfWork.PayOrganization.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PayOrganizationTitle, Value = c.Id.ToString() });
            SectionStatusList = _unitOfWork.SectionStatus.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.SectionStatusName, Value = c.Id.ToString() });
            //Catch the semester instance id to use for new course sections
            if (semesterInstanceId != 0)
            {
                objCourseSection.SemesterInstanceId = semesterInstanceId;
                objSemesterInstance = _unitOfWork.SemesterInstance.Get(c => c.IsArchived != true && c.Id == semesterInstanceId);
            }
            //Edit mode
            if (courseSectionId != null && courseSectionId != 0)
            {
                objCourseSection = _unitOfWork.CourseSection.GetById(courseSectionId);
            }
            //Nothing found in DB
            if (objCourseSection == null)
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
            if (objCourseSection.Id == 0)
            {
                _unitOfWork.CourseSection.Add(objCourseSection);
                TempData["success"] = "Course Section added Successfully";
            }
            //Modifying a Row
            else
            {
                _unitOfWork.CourseSection.Update(objCourseSection);
                TempData["success"] = "Course Section updated Successfully";
            }
            //Saves changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index", new { id = objCourseSection.SemesterInstanceId });
        }
    }
}

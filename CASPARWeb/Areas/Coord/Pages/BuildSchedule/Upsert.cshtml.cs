using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.AccessControl;
using Utility;

namespace CASPARWeb.Areas.Coord.Pages.BuildSchedule
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		[BindProperty]
        public CourseSection objCourseSection { get; set; }
		public Course objCourse { get; set; }
        public AcademicProgram objAcademicProgram { get; set; }
		public SemesterInstance objSemesterInstance { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<Modality> StudentModalityList { get; set; }
        public IEnumerable<SelectListItem> ClassroomList { get; set; }
        public IEnumerable<SelectListItem> TimeBlockList { get; set; }
        public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
        public IEnumerable<SelectListItem> PartOfTermList { get; set; }
        public IEnumerable<SelectListItem> PayModelList { get; set; }
        public IEnumerable<SelectListItem> PayOrganizationList { get; set; }
        public IEnumerable<SelectListItem> SectionStatusList { get; set; }

		//Start of Student Report------------------------------------------------------------------------------------------------------------------------------
		public IEnumerable<WishlistCourse> WishlistCourseList { get; set; }
		public int StudentCount { get; private set; }
		public IEnumerable<WishlistModality> WishlistModalityList { get; set; }
		public Dictionary<int, int> ModalityCount { get; private set; }
		public IEnumerable<PartOfDay> PartOfDayList { get; set; }
		public IEnumerable<WishlistPartOfDay> WishlistPartOfDayList { get; set; }
		public Dictionary<int, int> PartOfDayCount { get; private set; }
		public IEnumerable<Campus> CampusList { get; set; }
		public IEnumerable<WishlistCampus> WishlistCampusList { get; set; }
		public Dictionary<int, int> CampusCount { get; private set; }
		//End of Student Report------------------------------------------------------------------------------------------------------------------------------

		public UpsertModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            objCourseSection = new CourseSection();
            objCourse = new Course();
            objAcademicProgram = new AcademicProgram();
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
		public async Task<IActionResult> OnGet(int courseSectionId, int semesterInstanceId)
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
                objCourse = _unitOfWork.Course.Get(c => c.Id == objCourseSection.CourseId && c.IsArchived != true);
                objAcademicProgram = _unitOfWork.AcademicProgram.Get(c => c.Id == objCourse.ProgramId && c.IsArchived != true);
            }
            //Nothing found in DB
            if (objCourseSection == null)
            {
                return NotFound();
            }

			//Start of Student Report------------------------------------------------------------------------------------------------------------------------------
			//Get the course id from the course section id
			var courseId = _unitOfWork.CourseSection.GetById(courseSectionId).CourseId;

			//List all the modalities, part of days, and campuses
			StudentModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
			PartOfDayList = _unitOfWork.PartOfDay.GetAll(c => c.IsArchived != true);
			CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true);

			//List all the modalities, part of days, and campuses that the students have selected
			WishlistCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.CourseId == courseId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");
			WishlistModalityList = _unitOfWork.WishlistModality.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");
			WishlistPartOfDayList = _unitOfWork.WishlistPartOfDay.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");
			WishlistCampusList = _unitOfWork.WishlistCampus.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");

			//Count the number of courses that share the courseId where the user is a student
			StudentCount = 0;

			foreach (var user in WishlistCourseList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, SD.STUDENT_ROLE))
				{
					// This user is a student. Count the student.
					StudentCount++;
				}
			}

			ModalityCount = new Dictionary<int, int>();

			foreach (var user in WishlistModalityList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, SD.STUDENT_ROLE))
				{
					// This user is a student. Count the modality id.
					int modalityId = user.ModalityId;
					if (ModalityCount.ContainsKey(modalityId))
					{
						ModalityCount[modalityId]++;
					}
					else
					{
						ModalityCount[modalityId] = 1;
					}
				}
			}

			// Sort the dictionary in descending order by value
			ModalityCount = ModalityCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

			PartOfDayCount = new Dictionary<int, int>();

			foreach (var user in WishlistPartOfDayList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, SD.STUDENT_ROLE))
				{
					// This user is a student. Count the part of day id.
					int partOfDayId = user.PartOfDayId;
					if (PartOfDayCount.ContainsKey(partOfDayId))
					{
						PartOfDayCount[partOfDayId]++;
					}
					else
					{
						PartOfDayCount[partOfDayId] = 1;
					}
				}
			}

			// Sort the dictionary in descending order by value
			PartOfDayCount = PartOfDayCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

			CampusCount = new Dictionary<int, int>();

			foreach (var user in WishlistCampusList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, SD.STUDENT_ROLE))
				{
					// This user is a student. Count the campus id.
					int campusId = user.CampusId;
					if (CampusCount.ContainsKey(campusId))
					{
						CampusCount[campusId]++;
					}
					else
					{
						CampusCount[campusId] = 1;
					}
				}
			}

			// Sort the dictionary in descending order by value
			CampusCount = CampusCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

			//End of Student Report------------------------------------------------------------------------------------------------------------------------------

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

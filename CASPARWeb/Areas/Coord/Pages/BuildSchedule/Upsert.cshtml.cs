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
        public IEnumerable<ApplicationUser> ApplicationUserList { get; set; }
        public IEnumerable<SelectListItem> InstructorList { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<Modality> StudentModalityList { get; set; }
        public IEnumerable<SelectListItem> SCampusList { get; set; }
        public IEnumerable<SelectListItem> SBuildingList { get; set; }
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
        //Start of instructor report---------------------------------------------------------------------------------
        public class AssingedCourseSections {

			public String modalites { get; set; }
		}
		public class InstructorReport {
            public Wishlist wishlist { get; set; }
            public int loadReqAmount { get; set; }
            public int sumOfCourseLoads { get; set; }
            public int realiseTime { get; set; }
            public int totalLoadApplied { get; set; }
            public int? ranking { get; set; }
            public List<String>? modalityList { get; set; }
            public List<String>? locationList { get; set; }
            public List<String>? timeList { get; set; }

            public List<String>? weekDayList { get; set; }

			public List<CourseSection> assignedCourses { get; set; }
			public InstructorReport() {
                modalityList = new List<String>();
                locationList = new List<String>();
                timeList = new List<String>();
                wishlist = new Wishlist();
				weekDayList = new List<String>();
				assignedCourses = new List<CourseSection>();
			}
        }

        //public List<Wishlist> instructorsWishList;
        public List<InstructorReport> instructorReport;
        public String SemesterName;
        //End of instructor report-----------------------------------------------------------------------------------
        public UpsertModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            objCourseSection = new CourseSection();
            objCourse = new Course();
            objAcademicProgram = new AcademicProgram();
            objSemesterInstance = new SemesterInstance();
            CourseList = new List<SelectListItem>();
            ApplicationUserList = new List<ApplicationUser>();
			InstructorList = new List<SelectListItem>();
            ModalityList = new List<SelectListItem>();
			SCampusList = new List<SelectListItem>();
			SBuildingList = new List<SelectListItem>();
			ClassroomList = new List<SelectListItem>();
            TimeBlockList = new List<SelectListItem>();
            DaysOfWeekList = new List<SelectListItem>();
            PartOfTermList = new List<SelectListItem>();
            PayModelList = new List<SelectListItem>();
            PayOrganizationList = new List<SelectListItem>();
            SectionStatusList = new List<SelectListItem>();
            instructorReport = new List<InstructorReport>();
        }
		public async Task<IActionResult> OnGet(int courseSectionId, int semesterInstanceId)
        {
            //Populate the foreign keys to avoid foreign key conflicts
            CourseList = _unitOfWork.Course.GetAll(c => c.IsArchived != true, null, "AcademicProgram").Select(c => new SelectListItem { Text = c.AcademicProgram.ProgramCode + " " + c.CourseNumber + " " + c.CourseTitle, Value = c.Id.ToString() });
            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll();
            foreach (var user in ApplicationUserList)
            {
                if (await _userManager.IsInRoleAsync(user, SD.INSTRUCTOR_ROLE))
                {
					InstructorList = InstructorList.Concat(new[] { new SelectListItem { Text = user.FullName, Value = user.Id.ToString() } });
				}
            }
			ModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.ModalityName, Value = c.Id.ToString() });
            SCampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.CampusName});
            SBuildingList = _unitOfWork.Building.GetAll(c => c.IsArchived != true,null,"Campus").Select(c => new SelectListItem { Text = c.BuildingName, Value = c.Campus.CampusName});
            ClassroomList = _unitOfWork.Classroom.GetAll(c => c.IsArchived != true,null,"Building").Select(c => new SelectListItem { Text = c.ClassroomNumber + " - " + c.Building.BuildingName, Value = c.Id.ToString()});
            TimeBlockList = _unitOfWork.TimeBlock.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.TimeBlockValue, Value = c.Id.ToString() });
            DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.DaysOfWeekValue, Value = c.Id.ToString() });
            PartOfTermList = _unitOfWork.PartOfTerm.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PartOfTermTitle, Value = c.Id.ToString(), Selected = c.PartOfTermTitle == "Full Term" });
            PayModelList = _unitOfWork.PayModel.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PayModelTitle, Value = c.Id.ToString(), Selected = c.PayModelTitle == "Load" });
            PayOrganizationList = _unitOfWork.PayOrganization.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.PayOrganizationTitle, Value = c.Id.ToString(), Selected = c.PayOrganizationTitle == "EAST" });
            SectionStatusList = _unitOfWork.SectionStatus.GetAll(c => c.IsArchived != true).Select(c => new SelectListItem { Text = c.SectionStatusName, Value = c.Id.ToString(), Selected = c.SectionStatusName == "Pending" });


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
			var courseId = 0;

			if (courseSectionId == 0)
            {
                courseId = 0;

				//List all the modalities, part of days, and campuses
				StudentModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
				PartOfDayList = _unitOfWork.PartOfDay.GetAll(c => c.IsArchived != true);
				CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true);

				//List all the modalities, part of days, and campuses that the students have selected
				WishlistModalityList = _unitOfWork.WishlistModality.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");
				WishlistPartOfDayList = _unitOfWork.WishlistPartOfDay.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");
				WishlistCampusList = _unitOfWork.WishlistCampus.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");

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
			}
            else
            {
				courseId = _unitOfWork.CourseSection.GetById(courseSectionId).CourseId;
				WishlistCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId && w.CourseId == courseId && w.IsArchived != true, null, "Wishlist,Wishlist.ApplicationUser");

				//List all the modalities, part of days, and campuses
				StudentModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
				PartOfDayList = _unitOfWork.PartOfDay.GetAll(c => c.IsArchived != true);
				CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true);

				//List all the modalities, part of days, and campuses that the students have selected
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
			}

			

			//End of Student Report------------------------------------------------------------------------------------------------------------------------------
			//Start of instructor report---------------------------------------------------------------------------------


			//Using courseSectionId and semesterInstanceId
			//List instructors and ranking them from their wishlist rank
			//and where their their load requirements has not been met.
			//var courseId = _unitOfWork.CourseSection.GetById(courseSectionId).CourseId;
			//Get wishlists table with applicationUser table appended to it
			if (false)//courseSectionId == 0
			{
				courseId = 0;

				IEnumerable<Wishlist> wishlists = _unitOfWork.Wishlist.GetAll(w => w.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "ApplicationUser");

				List<Wishlist> tempWishlist = new List<Wishlist>();
				//foreach (WishlistCourse wishlistCourse in wishlistCourses)
				//{
				//	foreach (Wishlist wishlist in wishlists)
				//	{
				//		if (wishlist.Id == wishlistCourse.WishlistId)
				//		{
				//			tempWishlist.Add(wishlist);
				//			break;
				//		}
				//	}
				//}
				//Filter out all students from wishlist table
				IEnumerable<ReleaseTime> allInstructorsList = _unitOfWork.ReleaseTime.GetAll(c => c.SemesterInstanceId == semesterInstanceId && c.IsArchived != true, null, "ApplicationUser");
				foreach (ReleaseTime instructor in allInstructorsList)
				{
					foreach (Wishlist wishlist in tempWishlist)
					{
						if (wishlist.ApplicationUser.Id == instructor.ApplicationUser.Id)
						{
							InstructorReport temp = new InstructorReport();
							temp.wishlist = wishlist;
							instructorReport.Add(temp);
							break;
						}
					}
				}
				if (instructorReport.Count == 0)
				{
					/*There are NO instructor preferences*/
				}
				//Fill instructor report class
				for (int i = 0; i < instructorReport.Count; i++)
				{
					//Get and calculate the LoadApplied/LoadTotal
					instructorReport[i].realiseTime = _unitOfWork.ReleaseTime.Get(r => r.SemesterInstanceId == semesterInstanceId && r.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && r.IsArchived != true).ReleaseTimeAmount;
					instructorReport[i].loadReqAmount = _unitOfWork.LoadReq.Get(l => l.SemesterInstanceId == semesterInstanceId && l.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && l.IsArchived != true).LoadReqAmount;
					//WishlistCourse tempWishCourses = _unitOfWork.WishlistCourse.Get(c => c.WishlistId == instructorReport[i].wishlist.Id && c.CourseId == tempCourse.Id && c.IsArchived != true);
					IEnumerable<CourseSection> tempCourseSections = _unitOfWork.CourseSection.GetAll(c => c.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && c.IsArchived != true && c.SemesterInstanceId == semesterInstanceId, null, "Course,classroom,classroom.building,classroom.building.campus,timeblock,modality,daysofweek");
					if (tempCourseSections == null || tempCourseSections.Count() == 0) {/*PASS*/}
					else
					{
						foreach (CourseSection tempCourseSection2 in tempCourseSections)
						{
							instructorReport[i].sumOfCourseLoads += tempCourseSection2.Course.CourseCreditHours;
						}
					}
					instructorReport[i].totalLoadApplied = instructorReport[i].sumOfCourseLoads + instructorReport[i].realiseTime;
					//Get the ranking
					//instructorReport[i].ranking = tempWishCourses.PreferenceRank;
					//Get all the modalities
					IEnumerable<WishlistModality> tempModalitys = _unitOfWork.WishlistModality.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "Modality");
					if (tempModalitys == null || tempModalitys.Count() == 0)
					{

					}
					else
					{
						foreach (WishlistModality tempModality in tempModalitys)
						{
							String temp = tempModality.Modality.ModalityName;
							instructorReport[i].modalityList.Add(temp);
						}
					}
					//Get all locations
					IEnumerable<WishlistCampus> tempCampuses = _unitOfWork.WishlistCampus.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "Campus");
					if (tempCampuses == null || tempCampuses.Count() == 0)
					{

					}
					else
					{
						foreach (WishlistCampus tempCampus in tempCampuses)
						{
							String temp = tempCampus.Campus.CampusName;
							instructorReport[i].locationList.Add(temp);
						}
					}
					//Get all time blocks
					IEnumerable<WishlistTimeBlock> tempTimeBlocks = _unitOfWork.WishlistTimeBlock.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "TimeBlock");
					if (tempTimeBlocks == null || tempTimeBlocks.Count() == 0)
					{

					}
					else
					{
						foreach (WishlistTimeBlock tempTimeBlock in tempTimeBlocks)
						{
							String temp = tempTimeBlock.TimeBlock.TimeBlockValue;
							instructorReport[i].timeList.Add(temp);
						}
					}
				}
				//Rank instructor report items
				instructorReport = instructorReport.OrderBy(i => i.ranking).ToList();

				//Get semester name for report title
				SemesterName = _unitOfWork.SemesterInstance.Get(s => s.Id == semesterInstanceId && s.IsArchived != true).SemesterInstanceName;
			}
			else
			{
				Course tempCourse = _unitOfWork.Course.Get(c => c.Id == courseId && c.IsArchived != true);
				IEnumerable<WishlistCourse> wishlistCourses = _unitOfWork.WishlistCourse.GetAll(w => w.CourseId == tempCourse.Id && w.IsArchived != true, null, "Course");

				IEnumerable<Wishlist> wishlists = _unitOfWork.Wishlist.GetAll(w => w.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "ApplicationUser");

				List<Wishlist> tempWishlist = new List<Wishlist>();
				foreach (WishlistCourse wishlistCourse in wishlistCourses)
				{
					foreach (Wishlist wishlist in wishlists)
					{
						if (wishlist.Id == wishlistCourse.WishlistId)
						{
							tempWishlist.Add(wishlist);
							break;
						}
					}
				}
				//Filter out all students from wishlist table
				IEnumerable<ReleaseTime> allInstructorsList = _unitOfWork.ReleaseTime.GetAll(c => c.SemesterInstanceId == semesterInstanceId && c.IsArchived != true, null, "ApplicationUser");
				foreach (ReleaseTime instructor in allInstructorsList)
				{
					foreach (Wishlist wishlist in tempWishlist)
					{
						if (wishlist.ApplicationUser.Id == instructor.ApplicationUser.Id)
						{
							InstructorReport temp = new InstructorReport();
							temp.wishlist = wishlist;
							instructorReport.Add(temp);
							break;
						}
					}
				}
				if (instructorReport.Count == 0)
				{
					/*There are NO instructor preferences*/
				}
				//Fill instructor report class
				for (int i = 0; i < instructorReport.Count; i++)
				{
					//Get and calculate the LoadApplied/LoadTotal
					instructorReport[i].realiseTime = _unitOfWork.ReleaseTime.Get(r => r.SemesterInstanceId == semesterInstanceId && r.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && r.IsArchived != true).ReleaseTimeAmount;
					instructorReport[i].loadReqAmount = _unitOfWork.LoadReq.Get(l => l.SemesterInstanceId == semesterInstanceId && l.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && l.IsArchived != true).LoadReqAmount;
					WishlistCourse tempWishCourses = _unitOfWork.WishlistCourse.Get(c => c.WishlistId == instructorReport[i].wishlist.Id && c.CourseId == tempCourse.Id && c.IsArchived != true);
					//get all assigned instructor coursesSections
					IEnumerable<CourseSection> tempCourseSections = _unitOfWork.CourseSection.GetAll(c => c.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && c.IsArchived != true && c.SemesterInstanceId == semesterInstanceId, null, "Course,Classroom,Classroom.Building,Classroom.Building.Campus,TimeBlock,Modality,DaysOfWeek");
					if (tempCourseSections == null || tempCourseSections.Count() == 0) { }
					else
					{
						foreach (CourseSection tempCourseSection2 in tempCourseSections)
						{
							instructorReport[i].sumOfCourseLoads += tempCourseSection2.Course.CourseCreditHours;
							instructorReport[i].assignedCourses.Add(tempCourseSection2);
						}
					}
					instructorReport[i].totalLoadApplied = instructorReport[i].sumOfCourseLoads + instructorReport[i].realiseTime;
					//Get the ranking
					instructorReport[i].ranking = tempWishCourses.PreferenceRank;
					//Get all the modalities
					IEnumerable<WishlistModality> tempModalitys = _unitOfWork.WishlistModality.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "Modality");
					if (tempModalitys == null || tempModalitys.Count() == 0)
					{

					}
					else
					{
						foreach (WishlistModality tempModality in tempModalitys)
						{
							String temp = tempModality.Modality.ModalityName;
							if (!hasValue(instructorReport[i].modalityList, temp)) {
								instructorReport[i].modalityList.Add(temp);
							}


							
						}
					}
					//Get all locations
					IEnumerable<WishlistCampus> tempCampuses = _unitOfWork.WishlistCampus.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "Campus");
					if (tempCampuses == null || tempCampuses.Count() == 0)
					{

					}
					else
					{
						foreach (WishlistCampus tempCampus in tempCampuses)
						{
							String temp = tempCampus.Campus.CampusName;
							if (!hasValue(instructorReport[i].locationList, temp)) {
								instructorReport[i].locationList.Add(temp);
							}

						}
					}
					//Get all time blocks
					IEnumerable<WishlistTimeBlock> tempTimeBlocks = _unitOfWork.WishlistTimeBlock.GetAll(w => w.WishlistId == instructorReport[i].wishlist.Id && w.IsArchived != true, null, "TimeBlock");
					if (tempTimeBlocks == null || tempTimeBlocks.Count() == 0)
					{

                } else {
                    foreach (WishlistTimeBlock tempTimeBlock in tempTimeBlocks) {
                        String temp = tempTimeBlock.TimeBlock.TimeBlockValue;
						if(!hasValue(instructorReport[i].timeList, temp)) {
								instructorReport[i].timeList.Add(temp);
							}

                    }
                }
				//Get all days of week
				IEnumerable<WishlistDaysOfWeek> tempWeekDays = _unitOfWork.WishlistDaysOfWeek.GetAll(d => d.WishlistId == instructorReport[i].wishlist.Id && d.IsArchived != true, null, "DaysOfWeek");
                if (tempWeekDays == null || tempWeekDays.Count() == 0) {
                } else { 
                    foreach (WishlistDaysOfWeek tempWeekDay in tempWeekDays) {
                        String temp = tempWeekDay.DaysOfWeek.DaysOfWeekValue;
							if (!hasValue(instructorReport[i].weekDayList, temp)) {
								instructorReport[i].weekDayList.Add(temp);
							}
                        
                    }
                }
            }
            
            //Rank instructor report items
            instructorReport = instructorReport.OrderBy(i => i.ranking).ToList();

				//Get semester name for report title
				SemesterName = _unitOfWork.SemesterInstance.Get(s => s.Id == semesterInstanceId && s.IsArchived != true).SemesterInstanceName;
			}

            
            //End of instructor report---------------------------------------------------------------------------------



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
            return RedirectToPage("./Sections", new { semesterInstanceId = objCourseSection.SemesterInstanceId });
        }
		private bool hasValue(List<String> list, String str) {
			for (int i = 0; i < list.Count; i++) {
				if (list[i] == str) {
					return true;
				}
			}
			return false;
		}
    }
}

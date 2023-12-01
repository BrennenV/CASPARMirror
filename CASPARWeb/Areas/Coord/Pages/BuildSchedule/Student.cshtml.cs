using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;

namespace CASPARWeb.Areas.Coord.Pages.BuildSchedule
{
    public class StudentModel : PageModel
    {
		private readonly UnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		//Start of Student Report------------------------------------------------------------------------------------------------------------------------------
		public IEnumerable<WishlistCourse> WishlistCourseList { get; set; }
		public int StudentCount { get; private set; }
		public IEnumerable<Modality> ModalityList { get; set; }
		public IEnumerable<WishlistModality> WishlistModalityList { get; set; }
		public Dictionary<int, int> ModalityCount { get; private set; }
		public IEnumerable<PartOfDay> PartOfDayList { get; set; }
		public IEnumerable<WishlistPartOfDay> WishlistPartOfDayList { get; set; }
		public Dictionary<int, int> PartOfDayCount { get; private set; }
		public IEnumerable<Campus> CampusList { get; set; }
		public IEnumerable<WishlistCampus> WishlistCampusList { get; set; }
		public Dictionary<int, int> CampusCount { get; private set; }
		//End of Student Report------------------------------------------------------------------------------------------------------------------------------
		public StudentModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public async Task<IActionResult> OnGet(int courseSectionId, int semesterInstanceId)
		{
			//Delete this when the page is ready
			courseSectionId = 1;
			semesterInstanceId = 1;
		//Start of Student Report------------------------------------------------------------------------------------------------------------------------------
			//Get the course id from the course section id
			var courseId = _unitOfWork.CourseSection.GetById(courseSectionId).CourseId;

			//List all the modalities, part of days, and campuses
			ModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
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
			return Page();
		}
    }
}

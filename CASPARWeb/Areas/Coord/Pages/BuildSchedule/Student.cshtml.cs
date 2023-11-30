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
		public IEnumerable<Wishlist> WishlistList { get; set; }
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
			//List all the modalities, part of days, and campuses
			ModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
			PartOfDayList = _unitOfWork.PartOfDay.GetAll(c => c.IsArchived != true);
			CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true);

			//List all the modalities, part of days, and campuses that the students have selected
			WishlistModalityList = _unitOfWork.WishlistModality.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId, null, "Wishlist,Wishlist.ApplicationUser");
			WishlistPartOfDayList = _unitOfWork.WishlistPartOfDay.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId, null, "Wishlist,Wishlist.ApplicationUser");
			WishlistCampusList = _unitOfWork.WishlistCampus.GetAll(w => w.Wishlist.SemesterInstanceId == semesterInstanceId, null, "Wishlist,Wishlist.ApplicationUser");

			//Count the number of wishlist where the user is a student
			WishlistList = _unitOfWork.Wishlist.GetAll(w => w.SemesterInstanceId == semesterInstanceId, null, "ApplicationUser");
			StudentCount = 0;

			foreach (var user in WishlistList)
			{
				var applicationUser = user.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, Utility.SD.STUDENT_ROLE))
				{
					StudentCount++;
				}
			}

			ModalityCount = new Dictionary<int, int>();

			foreach (var user in WishlistModalityList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, Utility.SD.STUDENT_ROLE))
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

			PartOfDayCount = new Dictionary<int, int>();

			foreach (var user in WishlistPartOfDayList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, Utility.SD.STUDENT_ROLE))
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

			CampusCount = new Dictionary<int, int>();

			foreach (var user in WishlistCampusList)
			{
				var applicationUser = user.Wishlist.ApplicationUser;
				if (await _userManager.IsInRoleAsync(applicationUser, Utility.SD.STUDENT_ROLE))
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

	//End of Student Report------------------------------------------------------------------------------------------------------------------------------
			return Page();
		}
    }
}

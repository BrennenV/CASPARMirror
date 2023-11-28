using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;

namespace CASPARWeb.Areas.Stud.Pages.Wishlists
{
    public class IndexModel : PageModel
    {
		[BindProperty]
		public int SelectedSemesterId { get; set; }
		[BindProperty]
		public int WishlistId { get; set; }
		[BindProperty]
		public int Rank { get; set; }
		[BindProperty]
		public string SelectedCourse { get; set; }
		public IEnumerable<WishlistCourse> objCourseList;
		//Modality Checkboxes
		[BindProperty]
		public Wishlist objWishlist { get; set; }
		public IEnumerable<Modality> ModalityList { get; set; }
		public IEnumerable<WishlistModality> WishlistModalityList { get; set; }
		public List<String> AttachedModalityList { get; set; }
		public List<String> UnattachedModalityList { get; set; }
		[BindProperty]
		public String returnedAttachedModality { get; set; }
		//End Modality Checkboxes

		//PartOfDay Checkboxes
		public IEnumerable<PartOfDay> PartOfDayList { get; set; }
		public IEnumerable<WishlistPartOfDay> WishlistPartOfDayList { get; set; }
		public List<String> AttachedPartOfDayList { get; set; }
		public List<String> UnattachedPartOfDayList { get; set; }
		[BindProperty]
		public String returnedAttachedPartOfDay { get; set; }
		//End PartOfDay Checkboxes

		//DaysOfWeek Checkboxes
		public IEnumerable<DaysOfWeek> DaysOfWeekList { get; set; }
		public IEnumerable<WishlistDaysOfWeek> WishlistDaysOfWeekList { get; set; }
		public List<String> AttachedDaysOfWeekList { get; set; }
		public List<String> UnattachedDaysOfWeekList { get; set; }
		[BindProperty]
		public String returnedAttachedDaysOfWeek { get; set; }
		//End DaysOfWeek Checkboxes

		//Campus Checkboxes
		public IEnumerable<Campus> CampusList { get; set; }
		public IEnumerable<WishlistCampus> WishlistCampusList { get; set; }
		public List<String> AttachedCampusList { get; set; }
		public List<String> UnattachedCampusList { get; set; }
		[BindProperty]
		public String returnedAttachedCampus { get; set; }
		//End Campus Checkboxes

		public IEnumerable<SemesterInstance> objSemesterInstanceList;
		public IEnumerable<SelectListItem> CourseWishlistList { get; set; }
		private readonly UnitOfWork _unitOfWork;
		public IndexModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
            CourseWishlistList = new List<SelectListItem>();
			AttachedModalityList = new List<String>();
			UnattachedModalityList = new List<String>();
			AttachedPartOfDayList = new List<String>();
			UnattachedPartOfDayList = new List<String>();
			AttachedDaysOfWeekList = new List<String>();
			UnattachedDaysOfWeekList = new List<String>();
			AttachedCampusList = new List<String>();
			UnattachedCampusList = new List<String>();
		}
		public IActionResult OnGet(int? selectedSemesterId)
        {
            //This code is to help preserve the selected semester id upon returning to the index page
            if (selectedSemesterId != null)
            {
                SelectedSemesterId = (int)selectedSemesterId;
            }
            else
            {
				DateTime currentDate = DateTime.Now;
				SemesterInstance semesterInstance = _unitOfWork.SemesterInstance.GetAll(s => s.StartDate > currentDate).OrderBy(s => s.StartDate).FirstOrDefault();
				SelectedSemesterId = semesterInstance.Id;
			}
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Wishlist wishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);

			if (wishlist == null)
			{
				wishlist = new Wishlist
				{
					SemesterInstanceId = SelectedSemesterId,
					UserId = userId
				};

				_unitOfWork.Wishlist.Add(wishlist);
				_unitOfWork.Commit();
			}

			//Modality
			WishlistModalityList = _unitOfWork.WishlistModality.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistModality wishlistModality in WishlistModalityList)
			{
				//Grab each attached modality
				String temp = _unitOfWork.Modality.Get(c => c.Id == wishlistModality.ModalityId && c.IsArchived != true).ModalityName;
				AttachedModalityList.Add(temp);
			}
			//Fill UnattachedModalityList will all modality Names
			ModalityList = _unitOfWork.Modality.GetAll(c => c.IsArchived != true);
			foreach (Modality modality in ModalityList)
			{
				UnattachedModalityList.Add(modality.ModalityName);
			}
			//Remove amenities from UnattachedModalityList
			//if they are already stored in the AttachedModalityList
			for (int i = 0; i < AttachedModalityList.Count(); i++)
			{
				int l = 0;
				String attachedModality = AttachedModalityList[i];
				while (l < UnattachedModalityList.Count())
				{
					if (attachedModality == UnattachedModalityList[l])
					{
						UnattachedModalityList.RemoveAt(l);
						break;
					}
					l++;
				}
			}
			//End Modality

			//PartOfDay
			WishlistPartOfDayList = _unitOfWork.WishlistPartOfDay.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistPartOfDay wishlistPartOfDay in WishlistPartOfDayList)
			{
				//Grab each attached partOfDay
				String temp = _unitOfWork.PartOfDay.Get(c => c.Id == wishlistPartOfDay.PartOfDayId && c.IsArchived != true).PartOfDayValue;
				AttachedPartOfDayList.Add(temp);
			}
			//Fill UnattachedPartOfDayList will all partOfDay Names
			PartOfDayList = _unitOfWork.PartOfDay.GetAll(c => c.IsArchived != true);
			foreach (PartOfDay partOfDay in PartOfDayList)
			{
				UnattachedPartOfDayList.Add(partOfDay.PartOfDayValue);
			}
			//Remove amenities from UnattachedPartOfDayList
			//if they are already stored in the AttachedPartOfDayList
			for (int i = 0; i < AttachedPartOfDayList.Count(); i++)
			{
				int l = 0;
				String attachedPartOfDay = AttachedPartOfDayList[i];
				while (l < UnattachedPartOfDayList.Count())
				{
					if (attachedPartOfDay == UnattachedPartOfDayList[l])
					{
						UnattachedPartOfDayList.RemoveAt(l);
						break;
					}
					l++;
				}
			}

			//End PartOfDay

			//Campus
			WishlistCampusList = _unitOfWork.WishlistCampus.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistCampus wishlistCampus in WishlistCampusList)
			{
				//Grab each attached modality
				String temp = _unitOfWork.Campus.Get(c => c.Id == wishlistCampus.CampusId && c.IsArchived != true).CampusName;
				AttachedCampusList.Add(temp);
			}
			//Fill UnattachedCampusList will all modality Names
			CampusList = _unitOfWork.Campus.GetAll(c => c.IsArchived != true);
			foreach (Campus modality in CampusList)
			{
				UnattachedCampusList.Add(modality.CampusName);
			}
			//Remove amenities from UnattachedCampusList
			//if they are already stored in the AttachedCampusList
			for (int i = 0; i < AttachedCampusList.Count(); i++)
			{
				int l = 0;
				String attachedCampus = AttachedCampusList[i];
				while (l < UnattachedCampusList.Count())
				{
					if (attachedCampus == UnattachedCampusList[l])
					{
						UnattachedCampusList.RemoveAt(l);
						break;
					}
					l++;
				}
			}
			//End Campus

			objCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlist.Id, null, "Wishlist,Course,Course.AcademicProgram");

			return Page();
        }

		public IActionResult OnPostAdd(int? selectedSemesterId, int? selectedCourse)
		{

			SelectedSemesterId = (int)selectedSemesterId;

			// get the wishlistId for the current user and semester
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Wishlist wishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);
			// add the course to the wishlistCourse where the wishlistId matches the wishlistId for the current user and semester
			WishlistCourse wishlistCourse = new WishlistCourse
			{
				WishlistId = wishlist.Id,
				CourseId = (int)selectedCourse,
				PreferenceRank = Rank
			};

			_unitOfWork.WishlistCourse.Add(wishlistCourse);
			_unitOfWork.Commit();


			return RedirectToPage("./Index", new { selectedSemesterId = SelectedSemesterId });
		}

		public IActionResult OnPostArchiveCourse(int? selectedCourse)
		{
			_unitOfWork.WishlistCourse.Delete(_unitOfWork.WishlistCourse.Get(w => w.Id == (int)selectedCourse));
			_unitOfWork.Commit();

			return RedirectToPage("./Index");
		}

		public IActionResult OnPostCheckBoxes(int? selectedSemesterId)
		{
			SelectedSemesterId = (int)selectedSemesterId;
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			objWishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);

			String[] checkedModalityList = returnedAttachedModality != null ? returnedAttachedModality.Split(',') : new String[0];

			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				if (checkedModalityList.Length > 0 && checkedModalityList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedModalityList)
					{
						WishlistModality temp = new WishlistModality();
						temp.WishlistId = objWishlist.Id;

						if (_unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true).Id != null || _unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true).Id != 0)
						{
							temp.ModalityId = _unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true).Id;
							_unitOfWork.WishlistModality.Add(temp);
						}
					}
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else
			{
				//get all attached WishlistModality's
				IEnumerable<WishlistModality> tempList = _unitOfWork.WishlistModality.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistModality ap in tempList)
				{
					var modality = ap.Modality;
					if (modality != null && !checkedModalityList.Contains(modality.ModalityName))
					{
						ap.IsArchived = true;
						_unitOfWork.WishlistModality.Update(ap);
					}

				}
				//if the user assigned no amenities or "_"
				if (checkedModalityList.Length > 0 && checkedModalityList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedModalityList)
					{
						WishlistModality temp = new WishlistModality();
						temp.WishlistId = objWishlist.Id;

						var modality = _unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true);
						if (modality != null && modality.Id != 0)
						{
							temp.ModalityId = modality.Id;
							_unitOfWork.WishlistModality.Add(temp);
						}
					}
				}
				_unitOfWork.Wishlist.Update(objWishlist);
				TempData["success"] = "Wishlist updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();

			//********************************Time Block********************************

			String[] checkedPartOfDayList = returnedAttachedPartOfDay != null ? returnedAttachedPartOfDay.Split(',') : new String[0];
			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				//add the checked time blocks to the wishlist
				if (checkedPartOfDayList.Length > 0 && checkedPartOfDayList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedPartOfDayList)
					{
						WishlistPartOfDay temp = new WishlistPartOfDay();
						temp.WishlistId = objWishlist.Id;

						if (_unitOfWork.PartOfDay.Get(c => c.PartOfDayValue == s && c.IsArchived != true).Id != null || _unitOfWork.PartOfDay.Get(c => c.PartOfDayValue == s && c.IsArchived != true).Id != 0)
						{
							temp.PartOfDayId = _unitOfWork.PartOfDay.Get(c => c.PartOfDayValue == s && c.IsArchived != true).Id;
							_unitOfWork.WishlistPartOfDay.Add(temp);
						}
					}
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else
			{
				//get all attached WishlistPartOfDay's
				IEnumerable<WishlistPartOfDay> tempList = _unitOfWork.WishlistPartOfDay.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistPartOfDay ap in tempList)
				{
					var modality = ap.PartOfDay;
					if (modality != null && !checkedPartOfDayList.Contains(modality.PartOfDayValue))
					{
						ap.IsArchived = true;
						_unitOfWork.WishlistPartOfDay.Update(ap);
					}

				}
				//if the user assigned no amenities or "_"
				if (checkedPartOfDayList.Length > 0 && checkedPartOfDayList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedPartOfDayList)
					{
						WishlistPartOfDay temp = new WishlistPartOfDay();
						temp.WishlistId = objWishlist.Id;

						var modality = _unitOfWork.PartOfDay.Get(c => c.PartOfDayValue == s && c.IsArchived != true);
						if (modality != null && modality.Id != 0)
						{
							temp.PartOfDayId = modality.Id;
							_unitOfWork.WishlistPartOfDay.Add(temp);
						}
					}
				}
				_unitOfWork.Wishlist.Update(objWishlist);
				TempData["success"] = "Wishlist updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();

			//********************************DaysOfWeek********************************
			String[] checkedDaysOfWeekList = returnedAttachedDaysOfWeek != null ? returnedAttachedDaysOfWeek.Split(',') : new String[0];

			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				if (checkedDaysOfWeekList.Length > 0 && checkedDaysOfWeekList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedDaysOfWeekList)
					{
						WishlistDaysOfWeek temp = new WishlistDaysOfWeek();
						temp.WishlistId = objWishlist.Id;

						if (_unitOfWork.DaysOfWeek.Get(c => c.DaysOfWeekValue == s && c.IsArchived != true).Id != null || _unitOfWork.DaysOfWeek.Get(c => c.DaysOfWeekValue == s && c.IsArchived != true).Id != 0)
						{
							temp.DaysOfWeekId = _unitOfWork.DaysOfWeek.Get(c => c.DaysOfWeekValue == s && c.IsArchived != true).Id;
							_unitOfWork.WishlistDaysOfWeek.Add(temp);
						}
					}
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else
			{
				//get all attached WishlistDaysOfWeek's
				IEnumerable<WishlistDaysOfWeek> tempList = _unitOfWork.WishlistDaysOfWeek.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistDaysOfWeek ap in tempList)
				{
					var daysOfWeek = ap.DaysOfWeek;
					if (daysOfWeek != null && !checkedDaysOfWeekList.Contains(daysOfWeek.DaysOfWeekValue))
					{
						ap.IsArchived = true;
						_unitOfWork.WishlistDaysOfWeek.Update(ap);
					}

				}
				//if the user assigned no amenities or "_"
				if (checkedDaysOfWeekList.Length > 0 && checkedDaysOfWeekList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedDaysOfWeekList)
					{
						WishlistDaysOfWeek temp = new WishlistDaysOfWeek();
						temp.WishlistId = objWishlist.Id;

						var daysOfWeek = _unitOfWork.DaysOfWeek.Get(c => c.DaysOfWeekValue == s && c.IsArchived != true);
						if (daysOfWeek != null && daysOfWeek.Id != 0)
						{
							temp.DaysOfWeekId = daysOfWeek.Id;
							_unitOfWork.WishlistDaysOfWeek.Add(temp);
						}
					}
				}
				_unitOfWork.Wishlist.Update(objWishlist);
				TempData["success"] = "Wishlist updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();

			//********************************Campus********************************

			String[] checkedCampusList = returnedAttachedCampus != null ? returnedAttachedCampus.Split(',') : new String[0];

			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				if (checkedCampusList.Length > 0 && checkedCampusList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedCampusList)
					{
						WishlistCampus temp = new WishlistCampus();
						temp.WishlistId = objWishlist.Id;

						if (_unitOfWork.Campus.Get(c => c.CampusName == s && c.IsArchived != true).Id != null || _unitOfWork.Campus.Get(c => c.CampusName == s && c.IsArchived != true).Id != 0)
						{
							temp.CampusId = _unitOfWork.Campus.Get(c => c.CampusName == s && c.IsArchived != true).Id;
							_unitOfWork.WishlistCampus.Add(temp);
						}
					}
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else
			{
				//get all attached WishlistCampus's
				IEnumerable<WishlistCampus> tempList = _unitOfWork.WishlistCampus.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistCampus ap in tempList)
				{
					var campus = ap.Campus;
					if (campus != null && !checkedCampusList.Contains(campus.CampusName))
					{
						ap.IsArchived = true;
						_unitOfWork.WishlistCampus.Update(ap);
					}

				}
				//if the user assigned no amenities or "_"
				if (checkedCampusList.Length > 0 && checkedCampusList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedCampusList)
					{
						WishlistCampus temp = new WishlistCampus();
						temp.WishlistId = objWishlist.Id;

						var campus = _unitOfWork.Campus.Get(c => c.CampusName == s && c.IsArchived != true);
						if (campus != null && campus.Id != 0)
						{
							temp.CampusId = campus.Id;
							_unitOfWork.WishlistCampus.Add(temp);
						}
					}
				}
				_unitOfWork.Wishlist.Update(objWishlist);
				TempData["success"] = "Wishlist updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();
			return RedirectToPage("./Index", new { selectedSemesterId = SelectedSemesterId });
		}
	}

}


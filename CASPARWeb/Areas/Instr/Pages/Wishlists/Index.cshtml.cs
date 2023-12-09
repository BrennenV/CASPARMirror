using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace CASPARWeb.Areas.Instr.Pages.Wishlists
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

		//TimeBlock Checkboxes
		public IEnumerable<TimeBlock> TimeBlockList { get; set; }
		public IEnumerable<WishlistTimeBlock> WishlistTimeBlockList { get; set; }
		public List<String> AttachedTimeBlockList { get; set; }
		public List<String> UnattachedTimeBlockList { get; set; }
		[BindProperty]
		public String returnedAttachedTimeBlock { get; set; }
		//End TimeBlock Checkboxes

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
			AttachedTimeBlockList = new List<String>();
			UnattachedTimeBlockList = new List<String>();
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

			//TimeBlock
			WishlistTimeBlockList = _unitOfWork.WishlistTimeBlock.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistTimeBlock wishlistTimeBlock in WishlistTimeBlockList)
			{
				//Grab each attached timeBlock
				String temp = _unitOfWork.TimeBlock.Get(c => c.Id == wishlistTimeBlock.TimeBlockId && c.IsArchived != true).TimeBlockValue;
				AttachedTimeBlockList.Add(temp);
			}
			//Fill UnattachedTimeBlockList will all timeBlock Names
			TimeBlockList = _unitOfWork.TimeBlock.GetAll(c => c.IsArchived != true)
				.OrderBy(t => TimeSpan.Parse(t.TimeBlockValue.Split('-')[0].Trim())).ToList();
			foreach (TimeBlock timeBlock in TimeBlockList)
			{
				UnattachedTimeBlockList.Add(timeBlock.TimeBlockValue);
			}
			//Remove amenities from UnattachedTimeBlockList
			//if they are already stored in the AttachedTimeBlockList
			for (int i = 0; i < AttachedTimeBlockList.Count(); i++)
			{
				int l = 0;
				String attachedTimeBlock = AttachedTimeBlockList[i];
				while (l < UnattachedTimeBlockList.Count())
				{
					if (attachedTimeBlock == UnattachedTimeBlockList[l])
					{
						UnattachedTimeBlockList.RemoveAt(l);
						break;
					}
					l++;
				}
			}

			//End TimeBlock

			//DaysOfWeek
			WishlistDaysOfWeekList = _unitOfWork.WishlistDaysOfWeek.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistDaysOfWeek wishlistDaysOfWeek in WishlistDaysOfWeekList)
			{
				//Grab each attached modality
				String temp = _unitOfWork.DaysOfWeek.Get(c => c.Id == wishlistDaysOfWeek.DaysOfWeekId && c.IsArchived != true).DaysOfWeekValue;
				AttachedDaysOfWeekList.Add(temp);
			}
			//Fill UnattachedDaysOfWeekList will all modality Names
			DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll(c => c.IsArchived != true);
			foreach (DaysOfWeek modality in DaysOfWeekList)
			{
				UnattachedDaysOfWeekList.Add(modality.DaysOfWeekValue);
			}
			//Remove amenities from UnattachedDaysOfWeekList
			//if they are already stored in the AttachedDaysOfWeekList
			for (int i = 0; i < AttachedDaysOfWeekList.Count(); i++)
			{
				int l = 0;
				String attachedDaysOfWeek = AttachedDaysOfWeekList[i];
				while (l < UnattachedDaysOfWeekList.Count())
				{
					if (attachedDaysOfWeek == UnattachedDaysOfWeekList[l])
					{
						UnattachedDaysOfWeekList.RemoveAt(l);
						break;
					}
					l++;
				}
			}
			//End DaysOfWeek

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

        public IActionResult OnPostUpRank(int? selectedCourse)
        {
            //Update the rank of the selected course to be one less than its current rank
			WishlistCourse wishlistCourse = _unitOfWork.WishlistCourse.Get(w => w.Id == (int)selectedCourse);
			if (wishlistCourse.PreferenceRank > 1)
			{
                wishlistCourse.PreferenceRank = wishlistCourse.PreferenceRank - 1;
                _unitOfWork.WishlistCourse.Update(wishlistCourse);

                _unitOfWork.Commit();
            }

            return RedirectToPage("./Index");
        }
        public IActionResult OnPostDownRank(int? selectedCourse)
        {
            //Update the rank of the selected course to be one more than its current rank
            WishlistCourse wishlistCourse = _unitOfWork.WishlistCourse.Get(w => w.Id == (int)selectedCourse);

            //if (wishlistCourse.PreferenceRank <= _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlistCourse.WishlistId).Count())
            //{
                wishlistCourse.PreferenceRank = wishlistCourse.PreferenceRank + 1;
                _unitOfWork.WishlistCourse.Update(wishlistCourse);

                _unitOfWork.Commit();
            //}

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
			else if(checkedModalityList.Length == 0)
			{

			}
			else
			{
				//get all attached WishlistModality's
				IEnumerable<WishlistModality> tempList = _unitOfWork.WishlistModality.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistModality ap in tempList)
				{
					var id = ap.Id;
					var modalityId = ap.ModalityId;
					var modality = _unitOfWork.Modality.Get(c => c.Id == modalityId && c.IsArchived != true);

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

			String[] checkedTimeBlockList = returnedAttachedTimeBlock != null ? returnedAttachedTimeBlock.Split(',') : new String[0];
			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				//add the checked time blocks to the wishlist
				if (checkedTimeBlockList.Length > 0 && checkedTimeBlockList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedTimeBlockList)
					{
						WishlistTimeBlock temp = new WishlistTimeBlock();
						temp.WishlistId = objWishlist.Id;

						if (_unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true).Id != null || _unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true).Id != 0)
						{
							temp.TimeBlockId = _unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true).Id;
							_unitOfWork.WishlistTimeBlock.Add(temp);
						}
					}
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else if (checkedTimeBlockList.Length > 0)
			{
				//get all attached WishlistTimeBlock's
				IEnumerable<WishlistTimeBlock> tempList = _unitOfWork.WishlistTimeBlock.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistTimeBlock ap in tempList)
				{
					var id = ap.Id;
					var modality = _unitOfWork.TimeBlock.Get(c => c.Id == ap.TimeBlockId && c.IsArchived != true);
					if (modality != null && !checkedTimeBlockList.Contains(modality.TimeBlockValue))
					{
						ap.IsArchived = true;
						_unitOfWork.WishlistTimeBlock.Update(ap);
					}

				}
				//if the user assigned no amenities or "_"
				if (checkedTimeBlockList.Length > 0 && checkedTimeBlockList[0] != "_")
				{
					//add the checked modalities to the wishlist
					foreach (String s in checkedTimeBlockList)
					{
						WishlistTimeBlock temp = new WishlistTimeBlock();
						temp.WishlistId = objWishlist.Id;

						var modality = _unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true);
						if (modality != null && modality.Id != 0)
						{
							temp.TimeBlockId = modality.Id;
							_unitOfWork.WishlistTimeBlock.Add(temp);
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
			else if (checkedCampusList.Length > 0)
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
			return RedirectToPage("./Index", new {selectedSemesterId = SelectedSemesterId});
		}
	}

}


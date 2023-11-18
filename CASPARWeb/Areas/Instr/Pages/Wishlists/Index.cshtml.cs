using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

			//Testing
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
			//End Testing

			//Testing
			WishlistTimeBlockList = _unitOfWork.WishlistTimeBlock.GetAll(w => w.WishlistId == wishlist.Id && w.IsArchived != true);
			foreach (WishlistTimeBlock wishlistTimeBlock in WishlistTimeBlockList)
			{
				//Grab each attached timeBlock
				String temp = _unitOfWork.TimeBlock.Get(c => c.Id == wishlistTimeBlock.TimeBlockId && c.IsArchived != true).TimeBlockValue;
				AttachedTimeBlockList.Add(temp);
			}
			//Fill UnattachedTimeBlockList will all timeBlock Names
			TimeBlockList = _unitOfWork.TimeBlock.GetAll(c => c.IsArchived != true);
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
			//End Testing

			objCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlist.Id, null, "Wishlist,Course,Course.AcademicProgram");

			return Page();
        }

		public IActionResult OnPostAdd(int? selectedSemesterId, int? selectedCourse)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

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


			return RedirectToPage("./Index");
		}

		public IActionResult OnPostArchiveCourse(int? selectedCourse)
		{
			_unitOfWork.WishlistCourse.Delete(_unitOfWork.WishlistCourse.Get(w => w.Id == (int)selectedCourse));
			_unitOfWork.Commit();

			return RedirectToPage("./Index");
		}

		public IActionResult OnPostModality(int? selectedSemesterId)
		{
			SelectedSemesterId = (int)selectedSemesterId;
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			objWishlist = _unitOfWork.Wishlist.Get(w => w.SemesterInstanceId == SelectedSemesterId && w.UserId == userId);
			//if (!ModelState.IsValid)
			//{
			//	TempData["error"] = "Data Incomplete";
			//	return Page();
			//}
			//putting checked amenities into a list
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
				//arcive all attached WishlistModality's
				IEnumerable<WishlistModality> tempList = _unitOfWork.WishlistModality.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistModality ap in tempList)
				{
					ap.IsArchived = true;
					_unitOfWork.WishlistModality.Update(ap);
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

			String[] checkedTimeBlockList = returnedAttachedTimeBlock.Split(',');
			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				//add the checked time blocks to the wishlist
				foreach (String s in checkedTimeBlockList)
				{
					WishlistTimeBlock temp = new WishlistTimeBlock();
					temp.WishlistId = wishlistId;
					temp.TimeBlockId = _unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true).Id;

					_unitOfWork.WishlistTimeBlock.Add(temp);
					_unitOfWork.Commit();
				}
				TempData["success"] = "Wishlist added Successfully";
			}
			//Modifying a Row
			else
			{
				//arcive all attached WishlistTimeBlock's
				IEnumerable<WishlistTimeBlock> tempList = _unitOfWork.WishlistTimeBlock.GetAll(c => c.WishlistId == objWishlist.Id && c.IsArchived != true);
				foreach (WishlistTimeBlock ap in tempList)
				{
					ap.IsArchived = true;
					_unitOfWork.WishlistTimeBlock.Update(ap);
				}
				//if the user assigned no amenities or "_"
				if (checkedTimeBlockList[0] != "_")
				{
					//add the checked time blocks to the wishlist
					foreach (String s in checkedTimeBlockList)
					{
						WishlistTimeBlock temp = new WishlistTimeBlock();
						temp.WishlistId = objWishlist.Id;
						var timeBlock = _unitOfWork.TimeBlock.Get(c => c.TimeBlockValue == s && c.IsArchived != true);
						if (timeBlock != null && timeBlock.Id != 0)
						{
							temp.TimeBlockId = timeBlock.Id;
							_unitOfWork.WishlistTimeBlock.Add(temp);
						}
					}
				}
				_unitOfWork.Wishlist.Update(objWishlist);
				TempData["success"] = "Wishlist updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();
			return RedirectToPage("./Index");
		}
	}

}


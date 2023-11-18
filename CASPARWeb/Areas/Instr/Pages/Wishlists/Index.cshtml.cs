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

		[BindProperty]
		public Wishlist objWishlist { get; set; }
		public IEnumerable<Modality> ModalityList { get; set; }
		public IEnumerable<WishlistModality> WishlistModalityList { get; set; }
		public List<String> AttachedModalityList { get; set; }
		public List<String> UnattachedModalityList { get; set; }
		[BindProperty]
		public String returnedAttachedModality { get; set; }

		public IEnumerable<SemesterInstance> objSemesterInstanceList;
		public IEnumerable<SelectListItem> CourseWishlistList { get; set; }
		private readonly UnitOfWork _unitOfWork;
		public IndexModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
            CourseWishlistList = new List<SelectListItem>();
			AttachedModalityList = new List<String>();
			UnattachedModalityList = new List<String>();
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

			objCourseList = _unitOfWork.WishlistCourse.GetAll(w => w.WishlistId == wishlist.Id, null, "Wishlist,Course,Course.AcademicProgram");

			return Page();
        }

		public IActionResult OnPostAdd(int? selectedSemesterId, int? selectedCourse)
		{
			//if (!ModelState.IsValid)
			//{
			//	return Page();
			//}

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

		public IActionResult OnPost(int? selectedSemesterId)
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
			String[] checkedModalityList = returnedAttachedModality.Split(',');
			//Creating a Row
			if (objWishlist.Id == 0)
			{
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
				int wishlistId = _unitOfWork.Wishlist.Get(c => c == objWishlist && c.IsArchived != true).Id;
				//add the checked amenities to the wishlist
				foreach (String s in checkedModalityList)
				{
					WishlistModality temp = new WishlistModality();
					temp.WishlistId = wishlistId;
					temp.ModalityId = _unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true).Id;

					_unitOfWork.WishlistModality.Add(temp);
					_unitOfWork.Commit();
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
				if (checkedModalityList[0] != "_")
				{
					//add the checked amenities to the wishlist
					foreach (String s in checkedModalityList)
					{
						WishlistModality temp = new WishlistModality();
						temp.WishlistId = objWishlist.Id;
						temp.ModalityId = _unitOfWork.Modality.Get(c => c.ModalityName == s && c.IsArchived != true).Id;

						_unitOfWork.WishlistModality.Add(temp);
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


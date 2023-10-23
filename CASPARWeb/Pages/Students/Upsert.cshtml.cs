using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Students
{
	public class UpsertModel : PageModel
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UnitOfWork _unitOfWork;

		[BindProperty]
		public WishlistDetailModality objWishlistDetailModality { get; set; }
		public WishlistDetail objWishlistDetail { get; set; }
		public Wishlist objWishlist { get; set; }
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> WishlistPartOfDayList { get; set; }

		public UpsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			objWishlistDetailModality = new WishlistDetailModality();
			objWishlistDetail = new WishlistDetail();
			objWishlist = new Wishlist();
			CourseList = new List<SelectListItem>();
			SemesterInstanceList = new List<SelectListItem>();
			ModalityList = new List<SelectListItem>();
			CampusList = new List<SelectListItem>();
			WishlistPartOfDayList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int? id)
		{
			// Populate our SelectLists
			CourseList = _unitOfWork.Course.GetAll().Select(c => new SelectListItem
			{
				Text = c.CourseTitle,
				Value = c.Id.ToString()
			});
			SemesterInstanceList = _unitOfWork.SemesterInstance.GetAll().Select(s => new SelectListItem
			{
				Text = s.SemesterInstanceName,
				Value = s.Id.ToString()
			});

			ModalityList = _unitOfWork.Modality.GetAll().Select(m => new SelectListItem
			{
				Text = m.ModalityName,
				Value = m.Id.ToString()
			});
			CampusList = _unitOfWork.Campus.GetAll().Select(c => new SelectListItem
			{
				Text = c.CampusName,
				Value = c.Id.ToString()
			});
			WishlistPartOfDayList = new List<SelectListItem>
			{
				new SelectListItem { Text = "Morning", Value = "Morning" },
				new SelectListItem { Text = "Afternoon", Value = "Afternoon" },
				new SelectListItem { Text = "Evening", Value = "Evening" }
			};

			// Are we in create mode
			if (id == null || id == 0)
			{
				return Page();
			}

			//Edit mode

			if (id != 0) //Retrieve preference from DB
			{
				objWishlistDetailModality = _unitOfWork.WishlistDetailModality.GetById(id);
				objWishlistDetail = _unitOfWork.WishlistDetail.GetById(objWishlistDetailModality.WishlistDetailId);
				objWishlist = _unitOfWork.Wishlist.GetById(objWishlistDetail.WishlistId);
			}

			if (objWishlistDetailModality == null) // Maybe nothing returned from DB
			{
				return NotFound();
			}

			return Page();
		}

		public IActionResult OnPost(int? id)
		{
			//Determine root path of wwwroot
			string webRootPath = _webHostEnvironment.WebRootPath;
			//Retrieve the file(s) from the form
			var files = HttpContext.Request.Form.Files;

			//if the preference is new (create)
			if (objWishlistDetailModality.Id == 0)
			{
				
				// Add this new preference internally
				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.WishlistDetail.Add(objWishlistDetail);
				_unitOfWork.WishlistDetailModality.Add(objWishlistDetailModality);
			}
			//The item exists already, so we are updating it
			else
			{
				/*Get the preference again from the DB because
				 * binding is on, and we need to process the 
				 * physical image separately from the binded
				 * property holding the URL string */

				var objWishlistDetailModalityFromDb = _unitOfWork.WishlistDetailModality.Get(w => w.Id == objWishlistDetailModality.Id);
				var objWishlistDetailFromDb = _unitOfWork.WishlistDetail.Get(w => w.Id == objWishlistDetailModalityFromDb.WishlistDetailId);
				var objWishlistFromDb = _unitOfWork.Wishlist.Get(w => w.Id == objWishlistDetailFromDb.WishlistId);
				
				//Update the existing preference
				_unitOfWork.WishlistDetailModality.Update(objWishlistDetailModality);
				_unitOfWork.WishlistDetail.Update(objWishlistDetail);
				_unitOfWork.Wishlist.Update(objWishlist);
			}
			//Save the changes to the DB
			_unitOfWork.Commit();

			//Redirect to the preferences page
			return RedirectToPage("./Index");
		}
	}
}

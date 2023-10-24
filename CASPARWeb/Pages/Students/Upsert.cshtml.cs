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
		[BindProperty]
		public WishlistDetail objWishlistDetail { get; set; }
		[BindProperty]
		public Wishlist objWishlist { get; set; }
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> SemesterInstanceList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> TimeOfDayList { get; set; }

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
			TimeOfDayList = new List<SelectListItem>();
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
			TimeOfDayList = _unitOfWork.TimeOfDay.GetAll().Select(t => new SelectListItem
			{
				Text = t.PartOfDay,
				Value = t.Id.ToString()
			});

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
			}

			if (objWishlistDetailModality == null) // Maybe nothing returned from DB
			{
				return NotFound();
			}

			return Page();
		}

		public IActionResult OnPost(int? id)
		{
			//if the preference is new (create)
			if (objWishlistDetailModality.Id == 0)
			{
				//Add objWishlistDetail to the DB
				_unitOfWork.WishlistDetail.Add(objWishlistDetail);
				_unitOfWork.Commit();

				//Set the WishlistDetailModality.WishlistDetailId to the WishlistDetailId
				objWishlistDetailModality.WishlistDetailId = objWishlistDetail.Id;
				_unitOfWork.WishlistDetailModality.Add(objWishlistDetailModality);
			}
			//The item exists already, so we are updating it
			else
			{
				var objWishlistDetailModalityFromDb = _unitOfWork.WishlistDetailModality.Get(w => w.Id == objWishlistDetailModality.Id);
				var objWishlistDetailFromDb = _unitOfWork.WishlistDetail.Get(w => w.Id == objWishlistDetailModalityFromDb.WishlistDetailId);
				var objWishlistFromDb = _unitOfWork.Wishlist.Get(w => w.Id == objWishlistDetailFromDb.WishlistId);

				if (objWishlistDetailModalityFromDb != null && objWishlistDetailFromDb != null)
				{
					//... update other properties as needed


					if (objWishlistDetailFromDb.WishlistId != objWishlistDetail.WishlistId || objWishlistDetailFromDb.CourseId != objWishlistDetail.CourseId)
					{
						//objWishlistDetailFromDb.WishlistId = objWishlistDetail.WishlistId;
						//objWishlistDetailFromDb.CourseId = objWishlistDetail.CourseId;

						_unitOfWork.WishlistDetail.Update(objWishlistDetail);
						_unitOfWork.Commit();
					}
					//... update other properties as needed

					if (objWishlistDetailModalityFromDb.TimeOfDayId != objWishlistDetailModality.TimeOfDayId || objWishlistDetailModalityFromDb.ModalityId != objWishlistDetailModality.ModalityId || objWishlistDetailModalityFromDb.CampusId != objWishlistDetailModality.CampusId)
					{
						objWishlistDetailModalityFromDb.TimeOfDayId = objWishlistDetailModality.TimeOfDayId;
						objWishlistDetailModalityFromDb.ModalityId = objWishlistDetailModality.ModalityId;
						objWishlistDetailModalityFromDb.CampusId = objWishlistDetailModality.CampusId;

						_unitOfWork.WishlistDetailModality.Update(objWishlistDetailModality);
					}
				}
			}
			//Save the changes to the DB
			_unitOfWork.Commit();

			//Redirect to the preferences page
			return RedirectToPage("./Index");
		}
	}
}

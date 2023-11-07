using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Stud.Pages.WishListsOLD
{
	public class InsertModel : PageModel
	{
		private readonly UnitOfWork _unitOfWork;

		[BindProperty]
		public int SemesterInstanceId { get; set; }
		[BindProperty]
		public int CourseId { get; set; }
		[BindProperty]
		public int CampusId { get; set; }
		[BindProperty]
		public int DaysOfWeekId { get; set; }
		[BindProperty]
		public int TimeOfDayId { get; set; }
		[BindProperty]
		public List<int> CheckedModalities { get; set; }
		[BindProperty]
		public int PreferenceRank { get; set; }
		public string SemesterInstanceName;

		public int currentSemesterInstanceId;
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> TimeOfDayList { get; set; }

		public InsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			CheckedModalities = new List<int>();
			CourseList = new List<SelectListItem>();
			ModalityList = new List<SelectListItem>();
			CampusList = new List<SelectListItem>();
			TimeOfDayList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int semesterInstanceId)
		{
			// Populate our SelectLists
			CourseList = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == semesterInstanceId, null, "Course,SemesterInstance,Course.AcademicProgram")
							.Select(c => new SelectListItem
							{
								Text = c.Course.AcademicProgram.ProgramCode + " " + c.Course.CourseNumber + " " + c.Course.CourseTitle,
								Value = c.CourseId.ToString()
							});

			ModalityList = _unitOfWork.Modality.GetAll(null)
						   .Select(c => new SelectListItem
						   {
							   Text = c.ModalityName,
							   Value = c.Id.ToString()
						   });
			CampusList = _unitOfWork.Campus.GetAll(null)
						   .Select(c => new SelectListItem
						   {
							   Text = c.CampusName,
							   Value = c.Id.ToString()
						   });
			TimeOfDayList = _unitOfWork.TimeOfDay.GetAll(null)
							.Select(c => new SelectListItem
							{
								Text = c.PartOfDay,
								Value = c.Id.ToString()
							});

			SemesterInstanceId = semesterInstanceId;
			SemesterInstanceName = _unitOfWork.SemesterInstance.GetById(semesterInstanceId).SemesterInstanceName;

			return Page();
		}

		public IActionResult OnPost()
		{
			Wishlist objWishlist = new Wishlist();
			WishlistDetail objWishlistDetail = new WishlistDetail();
			WishlistDetailModality objWishlistDetailModality;

			//Check if a new Wishlist row needs to be created
			if (_unitOfWork.Wishlist.GetAll(c => c.SemesterInstanceId == SemesterInstanceId && c.StudentId == 1).Count() == 0) //TODO: this instructor id must pull from currently logged in instructor
			{
				//Create a new preference list and add it to db
				objWishlist.SemesterInstanceId = SemesterInstanceId;
				//TODO: this needs to be set to the currently logged in user
				objWishlist.StudentId = 1;

				_unitOfWork.Wishlist.Add(objWishlist);
				_unitOfWork.Commit();
			}
			else
			{
				objWishlist = _unitOfWork.Wishlist.GetAll(c => c.SemesterInstanceId == SemesterInstanceId).First();
			}

			//Check if a new WishlistDetail needs to be created (it doesn't if a preference list detail exists with the requested courseId)
			if (_unitOfWork.WishlistDetail.GetAll(c => c.Wishlist.StudentId == 1 && c.Wishlist.SemesterInstanceId == SemesterInstanceId && c.CourseId == CourseId, null, "Wishlist").Count() == 0) //TODO: instructor id to logged in id
			{
				//Create new WishlistDetail
				objWishlistDetail.WishlistId = objWishlist.Id;
				objWishlistDetail.CourseId = CourseId;

				_unitOfWork.WishlistDetail.Add(objWishlistDetail);
				_unitOfWork.Commit();
			}
			else
			{
				//Grab the already existing preference list detail
				objWishlistDetail = _unitOfWork.WishlistDetail.GetAll(c => c.Wishlist.StudentId == 1 && c.Wishlist.SemesterInstanceId == SemesterInstanceId && c.CourseId == CourseId, null, "Wishlist").First();
			}


			//Create new WishlistDetailModalities
			foreach (int ModalityId in CheckedModalities)
			{
				objWishlistDetailModality = new WishlistDetailModality();
				objWishlistDetailModality.WishlistDetailId = objWishlistDetail.Id;
				objWishlistDetailModality.ModalityId = ModalityId;
				if (_unitOfWork.Modality.GetById(ModalityId).ModalityName != "Online")
				{
					//Add the Days, Times, and Campus
					objWishlistDetailModality.TimeOfDayId = TimeOfDayId;
					objWishlistDetailModality.CampusId = CampusId;
				}

				_unitOfWork.WishlistDetailModality.Add(objWishlistDetailModality);
			}

			_unitOfWork.Commit();

			////Redirect to the preferences page
			return RedirectToPage("./Index", new { selectedSemesterId = SemesterInstanceId });
		}
	}
}

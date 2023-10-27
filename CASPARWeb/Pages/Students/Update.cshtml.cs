using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Students
{
	public class UpdateModel : PageModel
	{
		private readonly UnitOfWork _unitOfWork;
		[BindProperty]
		public WishlistDetailModality objWishlistDetailModality { get; set; }
		[BindProperty]
		public int CourseId { get; set; }
		[BindProperty]
		public int originalCourseId { get; set; }
		[BindProperty]
		public int currentSemesterInstanceId { get; set; }
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> TimeOfDayList { get; set; }

		public UpdateModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			objWishlistDetailModality = new WishlistDetailModality();
			CourseList = new List<SelectListItem>();
			ModalityList = new List<SelectListItem>();
			CampusList = new List<SelectListItem>();
			TimeOfDayList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int id, int semesterInstanceId)
		{
			objWishlistDetailModality = _unitOfWork.WishlistDetailModality.GetById(id);
			WishlistDetail objWishlistDetail = _unitOfWork.WishlistDetail.GetById(objWishlistDetailModality.WishlistDetailId);
			CourseId = objWishlistDetail.CourseId;
			originalCourseId = CourseId;
			currentSemesterInstanceId = semesterInstanceId;

			//Nothing found in DB
			if (objWishlistDetailModality == null)
			{
				return NotFound();
			}

			//Grab all the ids for the options
			CourseList = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == semesterInstanceId, null, "Course,SemesterInstance,Course.AcademicProgram")
							.Select(c => new SelectListItem
							{
								Text = c.Course.AcademicProgram.ProgramCode + " " + c.Course.CourseNumber,
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

			return Page();
		}
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Data Incomplete";
				return Page();
			}

			//Check if they changed the CourseId
			if (originalCourseId == CourseId)
			{
				//Course not changed, just update the Modality
				emptyLocaleAndDateIfOnline();
				_unitOfWork.WishlistDetailModality.Update(objWishlistDetailModality);
				TempData["success"] = "Modality updated Successfully";
			}
			else
			{
				WishlistDetail oldWishlistDetail = _unitOfWork.WishlistDetail.GetById(objWishlistDetailModality.WishlistDetailId);
				//Course changed, reassign modality or perhaps create new WishlistDetail and assign the modality to it
				if (_unitOfWork.WishlistDetail.GetAll(c => c.Wishlist.StudentId == 1 && c.Wishlist.SemesterInstanceId == currentSemesterInstanceId && c.CourseId == CourseId, null, "Wishlist").Count() == 0) //TODO: instructor id needs to be from logged in instructor
				{
					//The requested course does not yet have a detail already made for it so we add a new detail
					WishlistDetail newWishlistDetail = new WishlistDetail();
					newWishlistDetail.WishlistId = oldWishlistDetail.WishlistId;
					newWishlistDetail.CourseId = CourseId;

					_unitOfWork.WishlistDetail.Add(newWishlistDetail);
					_unitOfWork.Commit();

					//Then assign the modality to the new detail
					objWishlistDetailModality.WishlistDetailId = newWishlistDetail.Id;
				}
				else
				{
					//The detail exists so find the detail
					WishlistDetail objWishlistDetail = _unitOfWork.WishlistDetail.GetAll(c => c.Wishlist.StudentId == 1 && c.Wishlist.SemesterInstanceId == currentSemesterInstanceId && c.CourseId == CourseId, null, "Wishlist").First(); //TODO: instructor id needs to be from logged in instructor
					objWishlistDetailModality.WishlistDetailId = objWishlistDetail.Id;
				}

				emptyLocaleAndDateIfOnline();
				_unitOfWork.WishlistDetailModality.Update(objWishlistDetailModality);
				TempData["success"] = "Modality updated Successfully";
			}

			//Saves changes
			_unitOfWork.Commit();
			return RedirectToPage("./Index", new { selectedSemesterId = currentSemesterInstanceId });
		}

		private void emptyLocaleAndDateIfOnline()
		{
			//If the user chooses online as their modality, it can lead to a case where the dates and location are set on an online modality
			//this ensures that those are cleared before updating the WishlistDetailModality
			if (_unitOfWork.Modality.GetById(objWishlistDetailModality.ModalityId).ModalityName == "Online")
			{
				objWishlistDetailModality.CampusId = null;
				objWishlistDetailModality.TimeOfDayId = null;
			}
		}
	}
}


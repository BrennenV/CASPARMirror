using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

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

        public DeleteModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
			var wishlistDetailModality = _unitOfWork.WishlistDetailModality.GetById(id);
			if (wishlistDetailModality == null)
            {
                return NotFound();
            }
			_unitOfWork.WishlistDetailModality.Delete(wishlistDetailModality); //TODO: this will eventually just change it to archived
			TempData["success"] = "Modality Deleted Successfully";
            _unitOfWork.Commit();
			return RedirectToPage("./Index", new { selectedSemesterId = currentSemesterInstanceId });
        }
    }
}

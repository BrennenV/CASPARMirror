using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Areas.Instr.Pages.WishListsOLD
{
    public class DeleteModel : PageModel
    {
		private readonly UnitOfWork _unitOfWork;
		[BindProperty]
		public PreferenceListDetailModality objPreferenceListDetailModality { get; set; }
		[BindProperty]
		public int CourseId { get; set; }
		[BindProperty]
		public int originalCourseId { get; set; }
		[BindProperty]
		public int currentSemesterInstanceId { get; set; }
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
		public IEnumerable<SelectListItem> TimeBlockList { get; set; }

		public DeleteModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			objPreferenceListDetailModality = new PreferenceListDetailModality();
			CourseList = new List<SelectListItem>();
			ModalityList = new List<SelectListItem>();
			CampusList = new List<SelectListItem>();
			DaysOfWeekList = new List<SelectListItem>();
			TimeBlockList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int id, int semesterInstanceId)
		{
			objPreferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
			PreferenceListDetail objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(objPreferenceListDetailModality.PreferenceListDetailId);
			CourseId = objPreferenceListDetail.CourseId;
			originalCourseId = CourseId;
			currentSemesterInstanceId = semesterInstanceId;

			//Nothing found in DB
			if (objPreferenceListDetailModality == null)
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
			DaysOfWeekList = _unitOfWork.DaysOfWeek.GetAll(null)
							.Select(c => new SelectListItem
							{
								Text = c.DaysOfWeekTitle,
								Value = c.Id.ToString()
							});
			TimeBlockList = _unitOfWork.TimeBlock.GetAll(null)
							.Select(c => new SelectListItem
							{
								Text = c.TimeBlockValue,
								Value = c.Id.ToString()
							});

			return Page();
		}
		public IActionResult OnPost(int? id)
		{
			var preferenceListDetailModality = _unitOfWork.PreferenceListDetailModality.GetById(id);
			if (preferenceListDetailModality == null)
			{
				return NotFound();
			}
			_unitOfWork.PreferenceListDetailModality.Delete(preferenceListDetailModality); //TODO: this will eventually just change it to archived
			TempData["success"] = "Modality Deleted Successfully";
			_unitOfWork.Commit();
			return RedirectToPage("./Index", new { selectedSemesterId = currentSemesterInstanceId }); 
		}
	}
}

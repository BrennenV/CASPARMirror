using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Instructor.PreferenceLists
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
		public int TimeBlockId { get; set; }
		[BindProperty]
		public List<int> CheckedModalities { get; set; }
		[BindProperty]
		public int PreferenceRank { get; set; }
		public string SemesterInstanceName;

		public int currentSemesterInstanceId;
		public IEnumerable<SelectListItem> CourseList { get; set; }
		public IEnumerable<SelectListItem> ModalityList { get; set; }
		public IEnumerable<SelectListItem> CampusList { get; set; }
		public IEnumerable<SelectListItem> DaysOfWeekList { get; set; }
		public IEnumerable<SelectListItem> TimeBlockList { get; set; }

		public InsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			CheckedModalities = new List<int>();
			CourseList = new List<SelectListItem>();
			ModalityList = new List<SelectListItem>();
			CampusList = new List<SelectListItem>();
			DaysOfWeekList = new List<SelectListItem>();
			TimeBlockList = new List<SelectListItem>();
		}
		public IActionResult OnGet(int semesterInstanceId)
		{
			// Populate our SelectLists
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

			SemesterInstanceId = semesterInstanceId;
			SemesterInstanceName = _unitOfWork.SemesterInstance.GetById(semesterInstanceId).SemesterInstanceName;

			return Page();
		}

		public IActionResult OnPost()
		{
			PreferenceList objPreferenceList = new PreferenceList();
			PreferenceListDetail objPreferenceListDetail = new PreferenceListDetail();
			PreferenceListDetailModality objPreferenceListDetailModality;

			//Check if a new PreferenceList row needs to be created
			if(_unitOfWork.PreferenceList.GetAll(c => c.SemesterInstanceId == SemesterInstanceId && c.InstructorId == 1).Count() == 0) //TODO: this instructor id must pull from currently logged in instructor
			{
				//Create a new preference list and add it to db
				objPreferenceList.SemesterInstanceId = SemesterInstanceId;
				//TODO: this needs to be set to the currently logged in user
				objPreferenceList.InstructorId = 1;

				_unitOfWork.PreferenceList.Add(objPreferenceList);
				_unitOfWork.Commit();
			}
			else
			{
				objPreferenceList = _unitOfWork.PreferenceList.GetAll(c => c.SemesterInstanceId == SemesterInstanceId).First();
			}

			//Check if a new PreferenceListDetail needs to be created (it doesn't if a preference list detail exists with the requested courseId)
			if(_unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1 && c.PreferenceList.SemesterInstanceId == SemesterInstanceId && c.CourseId == CourseId, null, "PreferenceList").Count() == 0) //TODO: instructor id to logged in id
			{
				//Create new PreferenceListDetail
				objPreferenceListDetail.PreferenceListId = objPreferenceList.Id;
				objPreferenceListDetail.CourseId = CourseId;
				objPreferenceListDetail.PreferenceRank = PreferenceRank;

				_unitOfWork.PreferenceListDetail.Add(objPreferenceListDetail);
				_unitOfWork.Commit();
			}
			else
			{
				//Grab the already existing preference list detail
				objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetAll(c => c.PreferenceList.InstructorId == 1 && c.PreferenceList.SemesterInstanceId == SemesterInstanceId && c.CourseId == CourseId, null, "PreferenceList").First();
				if (objPreferenceListDetail.PreferenceRank != PreferenceRank)
				{
					//If the already existing detail has a difference preference rank update it.
					objPreferenceListDetail.PreferenceRank = PreferenceRank;
					_unitOfWork.PreferenceListDetail.Update(objPreferenceListDetail);
				}
			}
			

			//Create new PreferenceListDetailModalities
			foreach(int ModalityId in CheckedModalities)
			{
				objPreferenceListDetailModality = new PreferenceListDetailModality();
				objPreferenceListDetailModality.PreferenceListDetailId = objPreferenceListDetail.Id;
				objPreferenceListDetailModality.ModalityId = ModalityId;
				if(_unitOfWork.Modality.GetById(ModalityId).ModalityName != "Online")
				{
					//Add the Days, Times, and Campus
					objPreferenceListDetailModality.DaysOfWeekId = DaysOfWeekId;
					objPreferenceListDetailModality.TimeBlockId = TimeBlockId;
					objPreferenceListDetailModality.CampusId = CampusId;
				}

				_unitOfWork.PreferenceListDetailModality.Add(objPreferenceListDetailModality);
			}

			_unitOfWork.Commit();

			////Redirect to the preferences page
			return RedirectToPage("./Index", new { selectedSemesterId = SemesterInstanceId });
		}
	}
}

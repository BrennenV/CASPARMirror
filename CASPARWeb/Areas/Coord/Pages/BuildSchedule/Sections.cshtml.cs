using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Runtime.Intrinsics.X86;

namespace CASPARWeb.Areas.Coord.Pages.BuildSchedule
{
    public class SectionsModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        public IEnumerable<CourseSection> courseSectionList;
        public SemesterInstance objSemesterInstance;
        public Dictionary<int, List<CourseSection>> courseSectionDictionary;

        public SectionsModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            courseSectionList = new List<CourseSection>();
            objSemesterInstance = new SemesterInstance();
            courseSectionDictionary = new Dictionary<int, List<CourseSection>>();
        }

        public IActionResult OnGet(int? semesterInstanceId)
        {
            objSemesterInstance = _unitOfWork.SemesterInstance.GetById(semesterInstanceId);
            courseSectionList = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == semesterInstanceId && c.IsArchived != true, null, "Course,Course.AcademicProgram,ApplicationUser,Modality,Classroom,TimeBlock,DaysOfWeek,PartOfTerm,PayModel,PayOrganization,SectionStatus,Classroom.Building,Classroom.Building.Campus").OrderBy(o => o.Course.CourseNumber).ThenBy(p => p.Course.ProgramId);

            foreach(var courseSection in courseSectionList)
            {
                if(!courseSectionDictionary.ContainsKey(courseSection.Course.Id))
                {
                    courseSectionDictionary.Add(courseSection.Course.Id, new List<CourseSection>());
                }
                courseSectionDictionary[courseSection.Course.Id].Add(courseSection);
            }

            return Page();
        }
    }
}

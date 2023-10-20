using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPARWeb.Pages.Administrator.SemesterInstances 
{
    public class CreateSemesterInstanceModel : PageModel
    {

        private UnitOfWork _unitOfWork;//easy to use, database managment methods
        public IEnumerable<Semester> DropdownItems { get; set; }
        public Semester selectedSemester { get; set; }
        public SemesterInstance selectedSemesterInstance { get; set; }
        public int? id;
        [BindProperty]
        public Semester objSemester { get; set; }

        //asp-for input variables when the onpost is called
        [BindProperty]
        public string inputSemesterName { get; set; }
        [BindProperty]
        public string inputSemesterTemplate { get; set; }
        [BindProperty]
        public string inputSemesterStatus { get; set; }
        [BindProperty]
        public DateTime inputSemesterStartDate { get; set; }
        [BindProperty]
        public DateTime inputSemesterEndDate { get; set; }
        [BindProperty]
        public DateTime inputRegistrationStartDate { get; set; }
        [BindProperty]
        public DateTime inputRegistrationEndDate {  get; set; }
        [BindProperty]
        public string? inputPressedDelete {  get; set; }
        public SemesterInstance objSemesterInstance { get; set; }
        public CourseSection objCourseSection { get; set; }
        public Course objCourse { get; set; }
        public CreateSemesterInstanceModel(UnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;

        }
        
        public String[] initalDateTimes = new String[4]; 
        public void OnGet(int? id) {//occurs when the page first loads
            if (id != null) {
                //editing the semester
                this.id = id;
                selectedSemesterInstance = _unitOfWork.SemesterInstance.Get(s => s.Id == id);
                selectedSemester = _unitOfWork.Semester.Get(s => s.Id == selectedSemesterInstance.SemesterId);

                DateTime test = (DateTime)selectedSemesterInstance.StartDate;
                initalDateTimes[0] = test.ToString("yyyy-MM-dd");
                test = (DateTime)selectedSemesterInstance.EndDate;
                initalDateTimes[1] = test.ToString("yyyy-MM-dd");
                test = (DateTime)selectedSemesterInstance.RegistrationDate;
                initalDateTimes[2] = test.ToString("yyyy-MM-dd");
                test = (DateTime)selectedSemesterInstance.EndRegistrationDate;
                initalDateTimes[3] = test.ToString("yyyy-MM-dd");

            } else {
                for (int i = 0; i < initalDateTimes.Length; i++) {
                    DateTime test = DateTime.Now;
                    initalDateTimes[i] = test.ToString("yyyy-MM-dd");
                }
            }
            DropdownItems = _unitOfWork.Semester.GetAll(null, null, null);
        }
        public IActionResult OnPost(int? id) {//occurs when you press a button with type "submit"

            if (id != null) {
                //editing the semester
                //-> first delete the current semester
                SemesterInstance semesterInstance = _unitOfWork.SemesterInstance.Get(s => s.Id == id);

                IEnumerable<CourseSection> courseSections = _unitOfWork.CourseSection.GetAll(c => c.SemesterInstanceId == id);
                foreach (CourseSection courseSection in courseSections) {
                    _unitOfWork.CourseSection.Delete(courseSection);
                }
                _unitOfWork.SemesterInstance.Delete(semesterInstance);
                _unitOfWork.Commit();
                //check to see if all you doing is deleting that semesterInstance
                if (inputPressedDelete != "false") {
                    //Deleting the semester instance
                    //_unitOfWork.Commit();
                    return RedirectToPage("./Index");
                }
            
            }
            objSemesterInstance = new SemesterInstance();
            //create a SemesterInstance
            objSemesterInstance.SemesterInstanceName = inputSemesterName;
            objSemesterInstance.ScheduleStatus = inputSemesterStatus;
            objSemesterInstance.StartDate = inputSemesterStartDate;
            objSemesterInstance.EndDate = inputSemesterEndDate;
            objSemesterInstance.RegistrationDate = inputRegistrationStartDate;
            objSemesterInstance.EndRegistrationDate = inputRegistrationEndDate;

            //get the semester that has a semesterName equal to the selected semester template name
            Semester semester = _unitOfWork.Semester.Get(s => s.SemesterName == inputSemesterTemplate);
            objSemesterInstance.SemesterId = semester.Id;//semester Id (PK)
            _unitOfWork.SemesterInstance.Add(objSemesterInstance);
            _unitOfWork.Commit();
            //get the <list> of templates that have a semesterId equal to the selected semesterId 
            IEnumerable<Template> templates = _unitOfWork.Template.GetAll(t => t.SemesterId == semester.Id);
            
            //create all courseSections based on the templates
            foreach (Template template in templates) {
                if (template.Quantity > 0) {
                    for (int i = 0; i < template.Quantity; i++) {
                        //create the same course for each count in quantity
                        objCourseSection = new CourseSection();
                        objCourseSection.SemesterInstanceId = objSemesterInstance.Id;
                        objCourseSection.CourseId = template.CourseId;
                        objCourseSection.SectionUpdated = DateTime.Now;
                        _unitOfWork.CourseSection.Add(objCourseSection);
                    }
                }
                
            }

            //commit all those changes
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }

    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Areas.Coord.Pages.BuildSchedule {
    public class InstructorReport {
        public Wishlist wishlist { get; set; }
        public int loadReqAmount { get; set; }
        public int sumOfCourseLoads { get; set; }
        public int realiseTime { get; set; }
        public int totalLoadApplied { get; set; }
        public int? ranking { get; set; }
    }

    public class InstructorModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        //public List<Wishlist> instructorsWishList;
        public List<InstructorReport> instructorReport;
        public InstructorModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            //instructorsWishList = new List<Wishlist>();
            instructorReport = new List<InstructorReport>();
        }
        public void OnGet(int courseId1, int semesterInstanceId1)//courseID
        {
            int semesterInstanceId = 2;
            //int courseSectionId = 6;
            int courseId = 6;
            //Using courseSectionId and semesterInstanceId
            //List instructors and ranking them from their wishlist rank
            //and where their their load requirements has not been met.
            
            Course tempCourse = _unitOfWork.Course.Get(c => c.Id == courseId && c.IsArchived != true);

            IEnumerable<WishlistCourse> wishlistCourses = _unitOfWork.WishlistCourse.GetAll(w => w.CourseId == tempCourse.Id && w.IsArchived != true, null, "Course");
            IEnumerable<Wishlist> wishlists = _unitOfWork.Wishlist.GetAll(w => w.SemesterInstanceId == semesterInstanceId && w.IsArchived != true, null, "ApplicationUser");

            List<Wishlist> tempWishlist = new List<Wishlist>();
            foreach (WishlistCourse wishlistCourse in wishlistCourses) {
                foreach (Wishlist wishlist in wishlists) {
                    if (wishlist.Id == wishlistCourse.WishlistId) {
                        tempWishlist.Add(wishlist);
                        break;
                    }
                }
            }
            IEnumerable<ReleaseTime> allInstructorsList = _unitOfWork.ReleaseTime.GetAll(c => c.SemesterInstanceId == semesterInstanceId && c.IsArchived != true, null, "ApplicationUser");

            foreach (ReleaseTime instructor in allInstructorsList) {
                foreach (Wishlist wishlist in tempWishlist) {
                    if (wishlist.ApplicationUser.Id == instructor.ApplicationUser.Id) {
                        InstructorReport temp = new InstructorReport();
                        temp.wishlist = wishlist;
                        instructorReport.Add(temp);
                        break;
                    }
                }
            }
            if (instructorReport.Count == 0) {
                /*There are NO instructor preferences*/
            }

            for (int i = 0; i < instructorReport.Count; i++) {
                instructorReport[i].realiseTime = _unitOfWork.ReleaseTime.Get(r => r.SemesterInstanceId == semesterInstanceId && r.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && r.IsArchived != true).ReleaseTimeAmount;
                instructorReport[i].loadReqAmount = _unitOfWork.LoadReq.Get(l => l.SemesterInstanceId == semesterInstanceId && l.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && l.IsArchived != true).LoadReqAmount;
                WishlistCourse tempWishCourses = _unitOfWork.WishlistCourse.Get(c => c.WishlistId == instructorReport[i].wishlist.Id && c.CourseId == tempCourse.Id && c.IsArchived != true);
                instructorReport[i].ranking = tempWishCourses.PreferenceRank;
                IEnumerable<CourseSection> tempCourseSections = _unitOfWork.CourseSection.GetAll(c => c.InstructorId == instructorReport[i].wishlist.ApplicationUser.Id && c.IsArchived != true && c.SemesterInstanceId == semesterInstanceId, null, "Course");
                if (tempCourseSections == null || tempCourseSections.Count() == 0) {/*PASS*/} else {
                    foreach (CourseSection tempCourseSection2 in tempCourseSections) {
                        instructorReport[i].sumOfCourseLoads += tempCourseSection2.Course.CourseCreditHours;
                    }
                }
                //int temp = instructorReport[i].sumOfCourseLoads;
                instructorReport[i].totalLoadApplied = instructorReport[i].sumOfCourseLoads + instructorReport[i].realiseTime;

            }
        }
    }
}

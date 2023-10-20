using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace CASPARWeb.Pages.Administrator.SemesterInstances
{
    public class IndexModel : PageModel {
        /*private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterInstance objSemesterInstance { get; set; }
        public IndexModel(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            objSemesterInstance = new SemesterInstance();
        }
        public void OnGet(int? id) {
            if (id != null && id != 0) {
                Expression<Func<SemesterInstance, bool>> predicate = c => c.Id == id;
                objSemesterInstance = _unitOfWork.SemesterInstance.Get(predicate, true, "Semester,Open Enrollment Date,Close Enrollment Date");
            }
        }*/
    }
}

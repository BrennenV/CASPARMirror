using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace CASPARWeb.Areas.Admin.Pages.Templates
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Semester objSemester { get; set; }
        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objSemester = new Semester();
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Expression<Func<Semester, bool>> predicate = c => c.Id == id && c.IsArchived != true;
                objSemester = _unitOfWork.Semester.Get(predicate, true, "Course,Semester");
            }
        }
    }
}

using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetails
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceList objPreferenceList { get; set; }
        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceList = new PreferenceList();
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objPreferenceList = _unitOfWork.PreferenceList.Get(c => c.Id == id, true);
            }
        }
    }
}

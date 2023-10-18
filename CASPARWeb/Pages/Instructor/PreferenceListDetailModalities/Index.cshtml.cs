using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPARWeb.Pages.Instructor.PreferenceListDetailModalities
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public PreferenceListDetail objPreferenceListDetail { get; set; }
        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPreferenceListDetail = new PreferenceListDetail();
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                objPreferenceListDetail = _unitOfWork.PreferenceListDetail.GetById(id);
            }
        }
    }
}

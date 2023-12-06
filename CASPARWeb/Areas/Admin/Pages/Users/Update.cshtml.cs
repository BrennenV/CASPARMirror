using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace CASPARWeb.Areas.Admin.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateModel(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork; 
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }
        public List<string> UsersRoles { get; set; }
        public List<string> AllRoles { get; set; }
        public List<string> OldRoles { get; set; }

        public async Task OnGet(string id)
        {
            AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(AppUser);
            UsersRoles = roles.ToList();
            OldRoles = roles.ToList();
            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newRoles = Request.Form["roles"];
            UsersRoles = newRoles.ToList();
            var OldRoles = await _userManager.GetRolesAsync(AppUser); //ones in DB
            var rolesToAdd = new List<string>();
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == AppUser.Id);

            user.FirstName = AppUser.FirstName;
            user.LastName = AppUser.LastName;
            user.Email = AppUser.Email;
            user.PhoneNumber = AppUser.PhoneNumber;
            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Commit();

            bool becomingInstructor = false;
            bool removingInstructor = false;

            //update their roles
            foreach(var r in UsersRoles)
            {
                if(!OldRoles.Contains(r)) //new Role
                {
                    if(r == SD.INSTRUCTOR_ROLE)
                    {
                        becomingInstructor = true;
                    }
                    rolesToAdd.Add(r);
                }
            }

            foreach(var r in OldRoles)
            {
                if(!UsersRoles.Contains(r)) //remove
                {
                    if(r == SD.INSTRUCTOR_ROLE)
                    {
                        removingInstructor = true;
                    }
                    var result = await _userManager.RemoveFromRoleAsync(user, r);
                }
            }

            var result1 = await _userManager.AddToRolesAsync(user, rolesToAdd.AsEnumerable());

            if(becomingInstructor)
            {
                //Create LoadReq and ReleaseTimes for the new instructor in all existing semester instances
                IEnumerable<SemesterInstance> semesterInstances = _unitOfWork.SemesterInstance.GetAll(c => c.IsArchived != true, null, "Semester");
                IEnumerable<LoadReq> existingLoadReqs = _unitOfWork.LoadReq.GetAll(c => c.IsArchived != true && c.InstructorId == user.Id, null, "SemesterInstance");
                IEnumerable<ReleaseTime> existingReleaseTimes = _unitOfWork.ReleaseTime.GetAll(c => c.IsArchived != true && c.InstructorId == user.Id, null, "SemesterInstance");
                List<int> semesterInstanceIds = new List<int>();

                foreach(var loadReq in existingLoadReqs)
                {
                    semesterInstanceIds.Add(loadReq.SemesterInstanceId);
                }

                foreach (var instance in semesterInstances)
                {
                    if (!semesterInstanceIds.Contains(instance.Id))
                    {
                        ReleaseTime releaseTime = new ReleaseTime();
                        LoadReq loadReq = new LoadReq();

                        releaseTime.InstructorId = user.Id;
                        releaseTime.SemesterInstanceId = instance.Id;
                        releaseTime.ReleaseTimeAmount = 0;

                        loadReq.InstructorId = user.Id;
                        loadReq.SemesterInstanceId = instance.Id;
                        loadReq.LoadReqAmount = 0;
                        if (instance.Semester.SemesterName == "Fall" || instance.Semester.SemesterName == "Spring")
                        {
                            loadReq.LoadReqAmount = 12;
                        }

                        _unitOfWork.ReleaseTime.Add(releaseTime);
                        _unitOfWork.LoadReq.Add(loadReq);
                    } 
                }

                //For load reqs and release times that already exist for a user, unarchive them
                foreach (var loadReq in existingLoadReqs)
                {
                    loadReq.IsArchived = false;
                    _unitOfWork.LoadReq.Update(loadReq);
                }
                foreach (var releaseTime in existingReleaseTimes)
                {
                    releaseTime.IsArchived = false;
                    _unitOfWork.ReleaseTime.Update(releaseTime);
                }

                _unitOfWork.Commit();
            }
            else if(removingInstructor) 
            {
                //Archive all release times and load reqs associated with user
                IEnumerable<LoadReq> loadReqs = _unitOfWork.LoadReq.GetAll(c => c.IsArchived != true && c.InstructorId == user.Id, null);
                IEnumerable<ReleaseTime> releaseTimes = _unitOfWork.ReleaseTime.GetAll(c => c.IsArchived != true && c.InstructorId == user.Id, null);
                foreach (var loadReq in loadReqs)
                {
                    loadReq.IsArchived = true;
                    _unitOfWork.LoadReq.Update(loadReq);
                }
                foreach (var releaseTime in releaseTimes)
                {
                    releaseTime.IsArchived = true;
                    _unitOfWork.ReleaseTime.Update(releaseTime);
                }

                _unitOfWork.Commit();
            }

            return RedirectToPage("./Index", new { success = true, message = "Update Successful" });
        }
    }
}

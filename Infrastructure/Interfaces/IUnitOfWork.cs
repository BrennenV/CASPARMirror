using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<AcademicProgram> AcademicProgram { get; }

        public IGenericRepository<Building> Building { get; }

        public IGenericRepository<Campus> Campus { get; }

        public IGenericRepository<Classroom> Classroom { get; }

        public IGenericRepository<ClassroomAmenity> ClassroomAmenity { get; }

        public IGenericRepository<ClassroomAmenityPossession> ClassroomAmenityPossession { get; }

        public IGenericRepository<Course> Course { get; }

        public IGenericRepository<DaysOfWeek> DaysOfWeek { get; }

        public IGenericRepository<Instructor> Instructor { get; }

        public IGenericRepository<LoadReq> LoadReq { get; }

        public IGenericRepository<Modality> Modality { get; }

        public IGenericRepository<PartOfTerm> PartOfTerm { get; }

        public IGenericRepository<PayModel> PayModel { get; }

        public IGenericRepository<PayOrganization> PayOrganization { get; }

        public IGenericRepository<PreferenceList> PreferenceList { get; }

        public IGenericRepository<PreferenceListDetail> PreferenceListDetail { get; }

        public IGenericRepository<PreferenceListDetailModality> PreferenceListDetailModality { get; }

        public IGenericRepository<ProgramAssignment> ProgramAssignment { get; }

        public IGenericRepository<ReleaseTime> ReleaseTime { get; }

        public IGenericRepository<CourseSection> CourseSection { get; }

        public IGenericRepository<SectionStatus> SectionStatus { get; }

        public IGenericRepository<Semester> Semester { get; }

        public IGenericRepository<SemesterInstance> SemesterInstance { get; }

        public IGenericRepository<Student> Student { get; }

        public IGenericRepository<Template> Template { get; }

        public IGenericRepository<TimeBlock> TimeBlock { get; }

		public IGenericRepository<TimeOfDay> TimeOfDay { get; }

		public IGenericRepository<Wishlist> Wishlist { get; }

        public IGenericRepository<WishlistDetail> WishlistDetail { get; }

        public IGenericRepository<WishlistDetailModality> WishlistDetailModality { get; }

        //Added After Identity Framework Scaffolding
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }

        //These ones will eventually not be needed
        public IGenericRepository<Role> Role { get; }
        public IGenericRepository<User> User { get; }
        public IGenericRepository<RoleAssignment> RoleAssignment { get; }

        //Add other models/tables here as you create them
        //so UnitOfWork has access to them

        //save changes to the data source

        int Commit();

        Task<int> CommitAsync();
    }
}

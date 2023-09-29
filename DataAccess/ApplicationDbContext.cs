using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<AcademicProgram> AcademicPrograms { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Campus> Campuses { get; set; }

        public DbSet<Classroom> Classrooms { get; set; }

        public DbSet<ClassroomAmenity> ClassroomAmenities { get; set; }

        public DbSet<ClassroomAmenityPossession> ClassroomAmenityPossessions { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<DaysOfWeek> DaysOfWeeks { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<LoadReq> LoadReqs { get; set; }

        public DbSet<Modality> Modalities { get; set; }

        public DbSet<PartOfTerm> PartOfTerms { get; set; }

        public DbSet<PayModel> PayModels { get; set; }

        public DbSet<PayOrganization> PayOrganizations { get; set; }

        public DbSet<PreferenceList> PreferenceLists { get; set; }

        public DbSet<PreferenceListDetail> PreferenceListDetails { get; set; }

        public DbSet<ProgramAssignment> ProgramAssignments { get; set; }

        public DbSet<ReleaseTime> ReleaseTimes { get; set; }

        public DbSet<CourseSection> CourseSections { get; set; }

        public DbSet<SectionStatus> SectionStatuses { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<SemesterInstance> SemestersInstances { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<TimeBlock> TimeBlocks { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<WishlistDetail> WishlistDetails { get; set; }

        //These ones will eventually not be needed
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }

    }
}

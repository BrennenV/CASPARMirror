using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}


		public void Initialize()
		{
			_db.Database.EnsureCreated();

			//migrations if they are not applied
			try
			{
				if (_db.Database.GetPendingMigrations().Any())
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception)
			{

			}

			// Start Seeding the Database

			if (_db.Campuses.Any())
			{
				return; //DB has been seeded
			}

			// Seed the Roles

			_roleManager.CreateAsync(new IdentityRole(SD.ADMIN_ROLE)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.INSTRUCTOR_ROLE)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.STUDENT_ROLE)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.PROGRAM_COORDINATOR_ROLE)).GetAwaiter().GetResult();
			//This role might not even be used, but I added it just in case
			_roleManager.CreateAsync(new IdentityRole(SD.SECRETARY_ROLE)).GetAwaiter().GetResult();

			//****************************************************************************** Roles

			// Seed Users and their roles including a super admin
			//Creating the admin will all roles
			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "admin@admin.com",
				Email = "admin@admin.com",
				FirstName = "Admin",
				LastName = "istrator",
			}, "Admin123*").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
			_userManager.AddToRoleAsync(user, SD.ADMIN_ROLE).GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
			_userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
			_userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
            _userManager.AddToRoleAsync(user, SD.PROGRAM_COORDINATOR_ROLE).GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
            _userManager.AddToRoleAsync(user, SD.SECRETARY_ROLE).GetAwaiter().GetResult();

            //Creating Instructors
            _userManager.CreateAsync(new ApplicationUser
			{
				UserName = "instructor@instructor.com",
				Email = "instructor@instructor.com",
				FirstName = "Kyle",
				LastName = "Fuez",
			}, "Instructor123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor@instructor.com");
			_userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "instructor2@instructor.com",
                Email = "instructor2@instructor.com",
                FirstName = "Rich",
                LastName = "Fry",
            }, "Instructor123*").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor2@instructor.com");
            _userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "instructor3@instructor.com",
                Email = "instructor3@instructor.com",
                FirstName = "Arpit",
                LastName = "Christi",
            }, "Instructor123*").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor3@instructor.com");
            _userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "instructor4@instructor.com",
				Email = "instructor4@instructor.com",
				FirstName = "Brad",
				LastName = "Peterson",
			}, "Instructor123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor4@instructor.com");
			_userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "instructor5@instructor.com",
				Email = "instructor5@instructor.com",
				FirstName = "Linda",
				LastName = "Duhadway",
			}, "Instructor123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor5@instructor.com");
			_userManager.AddToRoleAsync(user, SD.INSTRUCTOR_ROLE).GetAwaiter().GetResult();

			//Creating Students
			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "student@student.com",
				Email = "student@student.com",
				FirstName = "Chris",
				LastName = "Jensen",
			}, "Student123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student@student.com");
			_userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "student2@student.com",
                Email = "student2@student.com",
                FirstName = "Joseph",
                LastName = "Brower",
            }, "Student123*").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student2@student.com");
            _userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "student3@student.com",
                Email = "student3@student.com",
                FirstName = "Jaeden",
                LastName = "Fisher",
            }, "Student123*").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student3@student.com");
            _userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "student4@student.com",
				Email = "student4@student.com",
				FirstName = "Aiden",
				LastName = "Mitchell",
			}, "Student123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student4@student.com");
			_userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "student5@student.com",
				Email = "student5@student.com",
				FirstName = "Sunny",
				LastName = "Shieldnicht",
			}, "Student123*").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student5@student.com");
			_userManager.AddToRoleAsync(user, SD.STUDENT_ROLE).GetAwaiter().GetResult();

            string[] studFirstNames = new string[] { "John", "Jane", "Sam", "Sara", "Bob", "Alice", "Tom", "Emma", "Max", "Olivia" };
            string[] studLastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson" };

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = studFirstNames[i],
                    LastName = studLastNames[i],
                };
                _userManager.CreateAsync(studentUser, "Student123*").GetAwaiter().GetResult();

                studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);
                _userManager.AddToRoleAsync(studentUser, SD.STUDENT_ROLE).GetAwaiter().GetResult();
            }

            //Create Program Coordinator
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "coord@coord.com",
                Email = "coord@coord.com",
                FirstName = "Hugo",
                LastName = "Valle",
            }, "Coord123*").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "coord@coord.com");
            _userManager.AddToRoleAsync(user, SD.PROGRAM_COORDINATOR_ROLE).GetAwaiter().GetResult();

            //Get some of the instructors and students for seeding further records
            var instr = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor@instructor.com");
            var instr2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor2@instructor.com");
            var instr3 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor3@instructor.com");
			var instr4 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor4@instructor.com");
			var instr5 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "instructor5@instructor.com");
			var stud = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student@student.com");
			var stud2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student2@student.com");
			var stud3 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student3@student.com");
			var stud4 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student4@student.com");
			var stud5 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "student5@student.com");

			//****************************************************************************** Super Admin

			// Seed the Campuses
			// - CampusName
			// - IsArchived

			var Campuses = new List<Campus>
			{
				new Campus { CampusName = "Weber Main Campus", IsArchived = false },
				new Campus { CampusName = "Weber Davis Campus", IsArchived = false},
				new Campus { CampusName = "Salt Lake Community College", IsArchived = false },
				new Campus { CampusName = "Farmington", IsArchived = false },
				new Campus { CampusName = "High School", IsArchived = false }
			};

			foreach (var c in Campuses)
			{
				_db.Campuses.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** Campuses

			// Seed the Buildings
			// - BuildingName
			// - CampusId (FK)
			// - IsArchived

			var Buildings = new List<Building>
			{
				new Building { BuildingName = "Elizabeth Hall", CampusId = 1, IsArchived = false },
				new Building { BuildingName = "Tracy Hall Science Center", CampusId = 1, IsArchived = false },
				new Building { BuildingName = "Computer & Automotive Engineering", CampusId = 1 , IsArchived = false },
				new Building { BuildingName = "Davis Building 1", CampusId = 2, IsArchived = false },
				new Building { BuildingName = "Davis Building 2", CampusId = 2, IsArchived = false },
				new Building { BuildingName = "Davis Building 3", CampusId = 2, IsArchived = false },
				new Building { BuildingName = "Salt Lake Building 1", CampusId = 3, IsArchived = false },
				new Building { BuildingName = "Salt Lake Building 2", CampusId = 3, IsArchived = false },
				new Building { BuildingName = "Farmington Building 1", CampusId = 4, IsArchived = false },
				new Building { BuildingName = "Ogden High", CampusId = 5, IsArchived = false },
				new Building { BuildingName = "Weber High", CampusId = 5, IsArchived = false },
				new Building { BuildingName = "Ben Lomond High", CampusId = 5, IsArchived = false }
			};

			foreach (var b in Buildings)
			{
				_db.Buildings.Add(b);
			}
			_db.SaveChanges();

			//****************************************************************************** Buildings

			// Seed the Classrooms
			// - BuildingId (FK)
			// - ClassroomDetail
			// - ClassroomSeats
			// - ClassroomNumber
			// - IsArchived

			var Classrooms = new List<Classroom>
			{
				new Classroom { BuildingId = 1, ClassroomDetail = "Classroom 1", ClassroomSeats = 30, ClassroomNumber = "EH 101", IsArchived = false },
				new Classroom { BuildingId = 1, ClassroomDetail = "Classroom 2", ClassroomSeats = 30, ClassroomNumber = "EH 232", IsArchived = false },
				new Classroom { BuildingId = 2, ClassroomDetail = "Classroom 3", ClassroomSeats = 30, ClassroomNumber = "TH 308", IsArchived = false },
				new Classroom { BuildingId = 3, ClassroomDetail = "Classroom 4", ClassroomSeats = 70, ClassroomNumber = "CAE 143", IsArchived = false },
				new Classroom { BuildingId = 4, ClassroomDetail = "Classroom 5", ClassroomSeats = 70, ClassroomNumber = "D1 104", IsArchived = false },
				new Classroom { BuildingId = 5, ClassroomDetail = "Classroom 6", ClassroomSeats = 40, ClassroomNumber = "D2 201", IsArchived = false },
				new Classroom { BuildingId = 6, ClassroomDetail = "Classroom 7", ClassroomSeats = 40, ClassroomNumber = "D3 121", IsArchived = false },
				new Classroom { BuildingId = 7, ClassroomDetail = "Classroom 8", ClassroomSeats = 40, ClassroomNumber = "SLCC1 109", IsArchived = false },
				new Classroom { BuildingId = 8, ClassroomDetail = "Classroom 9", ClassroomSeats = 40, ClassroomNumber = "SLCC2 213", IsArchived = false },
				new Classroom { BuildingId = 9, ClassroomDetail = "Classroom 10", ClassroomSeats = 40, ClassroomNumber = "F1 101", IsArchived = false },
				new Classroom { BuildingId = 10, ClassroomDetail = "Classroom 11", ClassroomSeats = 40, ClassroomNumber = "OHS 101", IsArchived = false },
				new Classroom { BuildingId = 11, ClassroomDetail = "Classroom 12", ClassroomSeats = 40, ClassroomNumber = "WHS 101", IsArchived = false },
				new Classroom { BuildingId = 12, ClassroomDetail = "Classroom 13", ClassroomSeats = 40, ClassroomNumber = "BLHS 101", IsArchived = false }
			};

			foreach (var c in Classrooms)
			{
				_db.Classrooms.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** Classrooms

			// Seed the ClassroomAmenities
			// - ClassroomAmenityName
			// - IsArchived

			var ClassroomAmenities = new List<ClassroomAmenity>
			{
				new ClassroomAmenity { ClassroomAmenityName = "Computer", IsArchived = false },
				new ClassroomAmenity { ClassroomAmenityName = "Multiple Projectors", IsArchived = false },
				new ClassroomAmenity { ClassroomAmenityName = "Multiple Whiteboards", IsArchived = false },
				new ClassroomAmenity { ClassroomAmenityName = "Lab Stations", IsArchived = false },
				new ClassroomAmenity { ClassroomAmenityName = "Auditorium", IsArchived = false }
			};

			foreach (var c in ClassroomAmenities)
			{
				_db.ClassroomAmenities.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** ClassroomAmenities

			// Seed the ClassroomAmenityPossessions
			// - ClassroomAmenityId (FK)
			// - ClassroomId (FK)
			// - IsArchived

			var ClassroomAmenityPossessions = new List<ClassroomAmenityPossession>
			{
				new ClassroomAmenityPossession { ClassroomAmenityId = 1, ClassroomId = 1, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 1, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 2, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 4, ClassroomId = 3, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 5, ClassroomId = 4, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 4, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 2, ClassroomId = 4, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 1, ClassroomId = 5, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 5, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 4, ClassroomId = 6, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 5, ClassroomId = 7, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 7, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 2, ClassroomId = 7, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 1, ClassroomId = 8, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 8, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 4, ClassroomId = 9, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 5, ClassroomId = 10, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 10, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 2, ClassroomId = 10, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 1, ClassroomId = 11, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 11, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 4, ClassroomId = 12, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 5, ClassroomId = 13, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 13, IsArchived = false },
				new ClassroomAmenityPossession { ClassroomAmenityId = 2, ClassroomId = 13, IsArchived = false }
			};

			foreach (var c in ClassroomAmenityPossessions)
			{
				_db.ClassroomAmenityPossessions.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** ClassroomAmenityPossessions

			// Seed the AcademicPrograms
			// - ProgramName
			// - ProgramCode
			// - IsArchived

			var AcademicPrograms = new List<AcademicProgram>
			{
				new AcademicProgram { ProgramName = "Computer Science", ProgramCode = "CS", IsArchived = false },
				new AcademicProgram { ProgramName = "Networking", ProgramCode = "NET", IsArchived = false },
				new AcademicProgram { ProgramName = "Web Development", ProgramCode = "WEB", IsArchived = false }
			};

			foreach (var a in AcademicPrograms)
			{
				_db.AcademicPrograms.Add(a);
			}
			_db.SaveChanges();

			//****************************************************************************** AcademicPrograms

			// Seed the Courses
			// - CourseTitle
			// - CourseCreditHours
			// - CourseNumber
			// - CourseDescription
			// - CourseNotes
			// - ProgramId (FK)
			// - IsArchived

			var Courses = new List<Course>
			{
				new Course { CourseTitle = "Introduction to Interactive Entertainment", CourseCreditHours = 3, CourseNumber = "1010", CourseDescription = "Introduction to Interactive Entertainment", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Foundations of Computing", CourseCreditHours = 3, CourseNumber = "1030", CourseDescription = "Foundations of Computing", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Programming I", CourseCreditHours = 3, CourseNumber = "1400", CourseDescription = "Programming I", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Object-Oriented Programming", CourseCreditHours = 3, CourseNumber = "1410", CourseDescription = "Object-Oriented Programming", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Computer Systems Administration", CourseCreditHours = 3, CourseNumber = "2140", CourseDescription = "Computer Systems Administration", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Client Side Web Development", CourseCreditHours = 3, CourseNumber = "2350", CourseDescription = "Client Side Web Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Individual Projects and Research", CourseCreditHours = 4, CourseNumber = "4800", CourseDescription = "Individual Projects and Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Software Engineering I", CourseCreditHours = 3, CourseNumber = "2450", CourseDescription = "Software Engineering I", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Client Side Frameworks", CourseCreditHours = 3, CourseNumber = "2630", CourseDescription = "Client Side Frameworks", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 3, CourseNumber = "2890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Associate Degree Assessment", CourseCreditHours = 3, CourseNumber = "2899", CourseDescription = "Associate Degree Assessment", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Scripting Language", CourseCreditHours = 3, CourseNumber = "3030", CourseDescription = "Scripting Language", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Enterprise Computing", CourseCreditHours = 3, CourseNumber = "3050", CourseDescription = "Enterprise Computing", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Operating Systems", CourseCreditHours = 3, CourseNumber = "3100", CourseDescription = "Operating Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "UNIX System Programming and Internals", CourseCreditHours = 3, CourseNumber = "3210", CourseDescription = "UNIX System Programming and Internals", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Mobile Development for the iPhone", CourseCreditHours = 3, CourseNumber = "3260", CourseDescription = "Mobile Development for the iPhone", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Mobile Development for Android", CourseCreditHours = 3, CourseNumber = "3270", CourseDescription = "Mobile Development for Android", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Object Oriented Windows Application Development", CourseCreditHours = 3, CourseNumber = "3280", CourseDescription = "Object Oriented Windows Application Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Database Programming", CourseCreditHours = 3, CourseNumber = "3550", CourseDescription = "Advanced Database Programming", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Data Science Algorithms", CourseCreditHours = 3, CourseNumber = "3580", CourseDescription = "Data Science Algorithms", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Introduction to Game Industry", CourseCreditHours = 3, CourseNumber = "3610", CourseDescription = "Introduction to Game Industry", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Rich Internet Application Development", CourseCreditHours = 3, CourseNumber = "3630", CourseDescription = "Rich Internet Application Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced User Interface Design", CourseCreditHours = 3, CourseNumber = "3645", CourseDescription = "Advanced User Interface Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Compiler Design", CourseCreditHours = 3, CourseNumber = "4820", CourseDescription = "Compiler Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Protocol Analysis", CourseCreditHours = 3, CourseNumber = "3705", CourseDescription = "Protocol Analysis", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Network Architectures and Protocols", CourseCreditHours = 3, CourseNumber = "3720", CourseDescription = "Network Architectures and Protocols", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Software Engineering II", CourseCreditHours = 3, CourseNumber = "3750", CourseDescription = "Software Engineering II", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = ".NET Web Application Development", CourseCreditHours = 3, CourseNumber = "4790", CourseDescription = ".NET Web Application Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Foundations of Game Development", CourseCreditHours = 3, CourseNumber = "4640", CourseDescription = "Foundations of Game Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Applied Cryptography", CourseCreditHours = 3, CourseNumber = "4730", CourseDescription = "Applied Cryptography", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - ASP.NET Core Web Applications", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - ASP.NET Core Web Applications", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - Java Application Development", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - Java Application Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - TBD", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - TBD", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Topics in Computer Science", CourseCreditHours = 3, CourseNumber = "4830", CourseDescription = "Advanced Topics in Computer Science", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 1, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 2, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 3, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 1, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 2, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 3, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 4, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Bachelor’s Degree Assessment", CourseCreditHours = 2, CourseNumber = "4899", CourseDescription = "Bachelor’s Degree Assessment", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Short Courses, Workshops, Institutes and Special Projects", CourseCreditHours = 3, CourseNumber = "4920", CourseDescription = "Short Courses, Workshops, Institutes and Special Projects", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Distributed Operating Systems", CourseCreditHours = 3, CourseNumber = "5100", CourseDescription = "Distributed Operating Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Internet of Things", CourseCreditHours = 3, CourseNumber = "5200", CourseDescription = "Internet of Things", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Algorithms", CourseCreditHours = 3, CourseNumber = "5420", CourseDescription = "Advanced Algorithms", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Software Evolution and Maintenance", CourseCreditHours = 3, CourseNumber = "5450", CourseDescription = "Software Evolution and Maintenance", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Artificial Intelligence", CourseCreditHours = 3, CourseNumber = "5500", CourseDescription = "Advanced Artificial Intelligence", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Database Management Systems", CourseCreditHours = 3, CourseNumber = "5550", CourseDescription = "Advanced Database Management Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Data Science Algorithms I", CourseCreditHours = 3, CourseNumber = "5570", CourseDescription = "Data Science Algorithms I", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Data Science Algorithms and Visualization", CourseCreditHours = 3, CourseNumber = "5580", CourseDescription = "Advanced Data Science Algorithms and Visualization", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Data Science Algorithms II", CourseCreditHours = 3, CourseNumber = "5580", CourseDescription = "Data Science Algorithms II", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Machine Learning", CourseCreditHours = 3, CourseNumber = "5600", CourseDescription = "Machine Learning", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Computer Architecture", CourseCreditHours = 3, CourseNumber = "5610", CourseDescription = "Computer Architecture", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Interaction Design", CourseCreditHours = 3, CourseNumber = "5650", CourseDescription = "Interaction Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Deep Learning Theory", CourseCreditHours = 3, CourseNumber = "5700", CourseDescription = "Deep Learning Theory", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Applied Cloud Computing", CourseCreditHours = 3, CourseNumber = "5705", CourseDescription = "Applied Cloud Computing", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Program Debugging and Repair", CourseCreditHours = 3, CourseNumber = "5720", CourseDescription = "Program Debugging and Repair", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Computer Systems Security", CourseCreditHours = 3, CourseNumber = "5740", CourseDescription = "Computer Systems Security", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - ASP.NET Core Web Applications", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - ASP.NET Core Web Applications", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - Java Application Development", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - Java Application Development", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Capstone Project - TBD", CourseCreditHours = 3, CourseNumber = "4760", CourseDescription = "Capstone Project - TBD", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Topics in Computer Science", CourseCreditHours = 3, CourseNumber = "4830", CourseDescription = "Advanced Topics in Computer Science", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 1, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 2, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Faculty Directed Research", CourseCreditHours = 3, CourseNumber = "4850", CourseDescription = "Faculty Directed Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 1, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 2, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 3, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "INT Cooperative Work Experience", CourseCreditHours = 4, CourseNumber = "4890", CourseDescription = "INT Cooperative Work Experience", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Bachelor’s Degree Assessment", CourseCreditHours = 2, CourseNumber = "4899", CourseDescription = "Bachelor’s Degree Assessment", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Short Courses, Workshops, Institutes and Special Projects", CourseCreditHours = 3, CourseNumber = "4920", CourseDescription = "Short Courses, Workshops, Institutes and Special Projects", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Distributed Operating Systems", CourseCreditHours = 3, CourseNumber = "5100", CourseDescription = "Distributed Operating Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Internet of Things", CourseCreditHours = 3, CourseNumber = "5200", CourseDescription = "Internet of Things", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Algorithms", CourseCreditHours = 3, CourseNumber = "5420", CourseDescription = "Advanced Algorithms", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Software Evolution and Maintenance", CourseCreditHours = 3, CourseNumber = "5450", CourseDescription = "Software Evolution and Maintenance", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Artificial Intelligence", CourseCreditHours = 3, CourseNumber = "5500", CourseDescription = "Advanced Artificial Intelligence", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Database Management Systems", CourseCreditHours = 3, CourseNumber = "5550", CourseDescription = "Advanced Database Management Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Data Science Algorithms I", CourseCreditHours = 3, CourseNumber = "5570", CourseDescription = "Data Science Algorithms I", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Data Science Algorithms and Visualization", CourseCreditHours = 3, CourseNumber = "5580", CourseDescription = "Advanced Data Science Algorithms and Visualization", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Data Science Algorithms II", CourseCreditHours = 3, CourseNumber = "5580", CourseDescription = "Data Science Algorithms II", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Machine Learning", CourseCreditHours = 3, CourseNumber = "5600", CourseDescription = "Machine Learning", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Computer Architecture", CourseCreditHours = 3, CourseNumber = "5610", CourseDescription = "Computer Architecture", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Interaction Design", CourseCreditHours = 3, CourseNumber = "5650", CourseDescription = "Interaction Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Deep Learning Theory", CourseCreditHours = 3, CourseNumber = "5700", CourseDescription = "Deep Learning Theory", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Applied Cloud Computing", CourseCreditHours = 3, CourseNumber = "5705", CourseDescription = "Applied Cloud Computing", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Program Debugging and Repair", CourseCreditHours = 3, CourseNumber = "5720", CourseDescription = "Program Debugging and Repair", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Computer Systems Security", CourseCreditHours = 3, CourseNumber = "5740", CourseDescription = "Computer Systems Security", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Compiler Design", CourseCreditHours = 3, CourseNumber = "5820", CourseDescription = "Compiler Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Special Topics in Computer Science", CourseCreditHours = 3, CourseNumber = "5830", CourseDescription = "Special Topics in Computer Science", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Formal System Design", CourseCreditHours = 3, CourseNumber = "5840", CourseDescription = "Formal System Design", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Parallel Programming and Architecture", CourseCreditHours = 3, CourseNumber = "5850", CourseDescription = "Parallel Programming and Architecture", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Fundamentals for Graduate Studies", CourseCreditHours = 3, CourseNumber = "6000", CourseDescription = "Fundamentals for Graduate Studies", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Design Project", CourseCreditHours = 2, CourseNumber = "6010", CourseDescription = "Design Project", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Design Project", CourseCreditHours = 4, CourseNumber = "6010", CourseDescription = "Design Project", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Thesis Research", CourseCreditHours = 2, CourseNumber = "6011", CourseDescription = "Thesis Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Thesis Research", CourseCreditHours = 4, CourseNumber = "6011", CourseDescription = "Thesis Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Thesis Research", CourseCreditHours = 6, CourseNumber = "6011", CourseDescription = "Thesis Research", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Distributed Operating Systems", CourseCreditHours = 3, CourseNumber = "6100", CourseDescription = "Distributed Operating Systems", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Internet of Things", CourseCreditHours = 3, CourseNumber = "6200", CourseDescription = "Internet of Things", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Advanced Algorithms", CourseCreditHours = 3, CourseNumber = "6420", CourseDescription = "Advanced Algorithms", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Software Evolution and Maintenance", CourseCreditHours = 3, CourseNumber = "6450", CourseDescription = "Software Evolution and Maintenance", ProgramId = 1, IsArchived = false },
				new Course { CourseTitle = "Web Design and Usability", CourseCreditHours = 3, CourseNumber = "1400", CourseDescription = "Introduction to web development", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Client Side Programming", CourseCreditHours = 3, CourseNumber = "1430", CourseDescription = "Client Side Programming", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Document Creation Comp Exam", CourseCreditHours = 1, CourseNumber = "1501", CourseDescription = "Document Creation Comp Exam", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Content, Internet, DevExam", CourseCreditHours = 1, CourseNumber = "1502", CourseDescription = "Content, Internet, DevExam", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Data Visual, Presentn Exam", CourseCreditHours = 1, CourseNumber = "1503", CourseDescription = "Data Visual, Presentn Exam", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Intro to Computer Application", CourseCreditHours = 3, CourseNumber = "1700", CourseDescription = "Intro to Computer Application", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Document Creation", CourseCreditHours = 1, CourseNumber = "1701", CourseDescription = "Document Creation", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Content, Internet & Device", CourseCreditHours = 1, CourseNumber = "1702", CourseDescription = "Content, Internet & Device", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Data, Visual, and Presentation", CourseCreditHours = 1, CourseNumber = "1703", CourseDescription = "Data, Visual, and Presentation", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Image Editing", CourseCreditHours = 3, CourseNumber = "2200", CourseDescription = "Image Editing", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Computer Illustrations", CourseCreditHours = 3, CourseNumber = "2210", CourseDescription = "Computer Illustrations", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Video Editing", CourseCreditHours = 3, CourseNumber = "2300", CourseDescription = "Video Editing", ProgramId = 3, IsArchived = false },
				new Course { CourseTitle = "Introduction to Cyber Defense and Ethics", CourseCreditHours = 3, CourseNumber = "1400", CourseDescription = "Introduction to networking", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Microcomputer Operating Systems", CourseCreditHours = 3, CourseNumber = "2200", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Linux System Administration", CourseCreditHours = 3, CourseNumber = "2210", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Introduction to LAN Management", CourseCreditHours = 3, CourseNumber = "2300", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network Server Administration", CourseCreditHours = 3, CourseNumber = "2310", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Cisco TCP/IP Routing Protocols and Router Configuration", CourseCreditHours = 3, CourseNumber = "2415", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Cisco Advanced LAN and WAN Switching and Routing Theory and Design", CourseCreditHours = 3, CourseNumber = "2435", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Practical Cybersecurity Infrastructure", CourseCreditHours = 3, CourseNumber = "2500", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Cyberethics", CourseCreditHours = 1, CourseNumber = "2510", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Cloud Architecture and Security", CourseCreditHours = 3, CourseNumber = "3210", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Advanced LAN Security Management", CourseCreditHours = 3, CourseNumber = "3300", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network Server Administration", CourseCreditHours = 3, CourseNumber = "3310", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Supervising Information Technology", CourseCreditHours = 3, CourseNumber = "3550", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Switching and Transmission Network Systems Management", CourseCreditHours = 3, CourseNumber = "3710", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Switching and Transmission Network Systems Management", CourseCreditHours = 4, CourseNumber = "3710", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Transmission Network Applications", CourseCreditHours = 2, CourseNumber = "3715", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Advanced Transport Media", CourseCreditHours = 3, CourseNumber = "3720", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Wireless Networking and Security", CourseCreditHours = 3, CourseNumber = "3720", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Cyber Policy and Ethics", CourseCreditHours = 3, CourseNumber = "3730", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Survey of Information Security Policies", CourseCreditHours = 3, CourseNumber = "3730", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Data and Voice Network Design", CourseCreditHours = 4, CourseNumber = "4700", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Security Vulnerabilities and Intrusion Mitigation", CourseCreditHours = 4, CourseNumber = "4740", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network Management Technology Internship", CourseCreditHours = 3, CourseNumber = "4760", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network/Telecommunications Internship", CourseCreditHours = 3, CourseNumber = "4760 INT", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network Management Technology Senior Project", CourseCreditHours = 3, CourseNumber = "4790", ProgramId = 2, IsArchived = false },
				new Course { CourseTitle = "Network/Telecommunications Senior Project", CourseCreditHours = 2, CourseNumber = "4790 INT", ProgramId = 2, IsArchived = false }
			};

			foreach (var c in Courses)
			{
				_db.Courses.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** Courses

			// Seed the ProgramAssignments
			// - InstructorId (FK)
			// - ProgramId (FK)
			// - IsArchived

			var ProgramAssignments = new List<ProgramAssignment>
			{
				new ProgramAssignment { InstructorId = instr.Id, ProgramId = 1, IsArchived = false },
				new ProgramAssignment { InstructorId = instr.Id, ProgramId = 2, IsArchived = false },
				new ProgramAssignment { InstructorId = instr.Id, ProgramId = 3, IsArchived = false },
				new ProgramAssignment { InstructorId = instr2.Id, ProgramId = 1, IsArchived = false },
				new ProgramAssignment { InstructorId = instr2.Id, ProgramId = 2, IsArchived = false },
				new ProgramAssignment { InstructorId = instr2.Id, ProgramId = 3, IsArchived = false },
				new ProgramAssignment { InstructorId = instr3.Id, ProgramId = 1, IsArchived = false },
				new ProgramAssignment { InstructorId = instr3.Id, ProgramId = 2, IsArchived = false },
				new ProgramAssignment { InstructorId = instr3.Id, ProgramId = 3, IsArchived = false },
				new ProgramAssignment { InstructorId = instr4.Id, ProgramId = 1, IsArchived = false },
				new ProgramAssignment { InstructorId = instr4.Id, ProgramId = 2, IsArchived = false },
				new ProgramAssignment { InstructorId = instr4.Id, ProgramId = 3, IsArchived = false },
				new ProgramAssignment { InstructorId = instr5.Id, ProgramId = 1, IsArchived = false },
				new ProgramAssignment { InstructorId = instr5.Id, ProgramId = 2, IsArchived = false },
				new ProgramAssignment { InstructorId = instr5.Id, ProgramId = 3, IsArchived = false }
			};

			foreach (var p in ProgramAssignments)
			{
				_db.ProgramAssignments.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** ProgramAssignments

			// Seed the PartOfTerms
			// - PartOfTermTitle
			// - IsArchived

			var PartOfTerms = new List<PartOfTerm>
			{
				new PartOfTerm { PartOfTermTitle = "Full Term", IsArchived = false },
				new PartOfTerm { PartOfTermTitle = "First Half Term", IsArchived = false },
				new PartOfTerm { PartOfTermTitle = "Second Half Term", IsArchived = false }
			};

			foreach (var p in PartOfTerms)
			{
				_db.PartOfTerms.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PartOfTerms

			// Seed the PayModels
			// - PayModelTitle
			// - IsArchived

			var PayModels = new List<PayModel>
			{
				new PayModel { PayModelTitle = "Load", IsArchived = false },
				new PayModel { PayModelTitle = "Overload", IsArchived = false },
				new PayModel { PayModelTitle = "Split Load", IsArchived = false },
				new PayModel { PayModelTitle = "E-Pars", IsArchived = false },
				new PayModel { PayModelTitle = "T1", IsArchived = false },
				new PayModel { PayModelTitle = "Abjunct Pay", IsArchived = false },
				new PayModel { PayModelTitle = "Special", IsArchived = false },
				new PayModel { PayModelTitle = "Other", IsArchived = false },
				new PayModel { PayModelTitle = "TBD", IsArchived = false }
			};

			foreach (var p in PayModels)
			{
				_db.PayModels.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PayModels

			// Seed the PayOrganizations
			// - PayOrganizationTitle
			// - IsArchived

			var PayOrganizations = new List<PayOrganization>
			{
				new PayOrganization { PayOrganizationTitle = "EAST", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "Grant", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "OCE", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "Provost", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "SWI", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "OCE+Provost", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "all", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "SOC+EAST", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "SoC", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "MSCS Budget", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "Other", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "TBD", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "MSDS Budget", IsArchived = false },
				new PayOrganization { PayOrganizationTitle = "SOC+OCE", IsArchived = false }
			};

			foreach (var p in PayOrganizations)
			{
				_db.PayOrganizations.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PayOrganizations

			// Seed the SectionStatuses
			// - SectionStatusName
			// - SectionStatusColor
			// - IsArchived

			var SectionStatuses = new List<SectionStatus>
			{
				new SectionStatus { SectionStatusName = "Pending", SectionStatusColor = "#ffff00", IsArchived = false },
				new SectionStatus { SectionStatusName = "Active", SectionStatusColor = "#00ffff", IsArchived = false },
				new SectionStatus { SectionStatusName = "Inactive", SectionStatusColor = "#808080", IsArchived = false },
				new SectionStatus { SectionStatusName = "Not Started", SectionStatusColor = "#ff0000", IsArchived = false },
				new SectionStatus { SectionStatusName = "Confirmed", SectionStatusColor = "#0000ff", IsArchived = false },
				new SectionStatus { SectionStatusName = "Needed", SectionStatusColor = "#800080", IsArchived = false },
				new SectionStatus { SectionStatusName = "IF Needed", SectionStatusColor = "#ffc0cb", IsArchived = false },
				new SectionStatus { SectionStatusName = "Cancelled", SectionStatusColor = "#3cb043", IsArchived = false },
				new SectionStatus { SectionStatusName = "Banner Conflict", SectionStatusColor = "#ffa500", IsArchived = false }
			};

			foreach (var s in SectionStatuses)
			{
				_db.SectionStatuses.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** SectionStatuses

			// Seed the DaysOfWeeks
			// - DaysOfWeekTitle
			// - IsArchived

			var DaysOfWeeks = new List<DaysOfWeek>
			{
				new DaysOfWeek { DaysOfWeekValue = "Monday", IsArchived = false  },
				new DaysOfWeek { DaysOfWeekValue = "Tuesday", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Wednesday", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Thursday", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Friday", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Saturday", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Sunday", IsArchived = false },
                new DaysOfWeek { DaysOfWeekValue = "Mon, Wed", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Tues, Thurs", IsArchived = false },
				new DaysOfWeek { DaysOfWeekValue = "Mon, Wed, Fri", IsArchived = false }
            };

			foreach (var d in DaysOfWeeks)
			{
				_db.DaysOfWeeks.Add(d);
			}
			_db.SaveChanges();

			//****************************************************************************** DaysOfWeeks

			// Seed the TimeBlocks
			// - TimeBlockValue
			// - IsArchived

			var TimeBlocks = new List<TimeBlock>
			{
				new TimeBlock { TimeBlockValue = "09:30 - 10:20", IsArchived = false  },
				new TimeBlock { TimeBlockValue = "09:30 - 11:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "10:30 - 11:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "11:30 - 12:45", IsArchived = false },
				new TimeBlock { TimeBlockValue = "17:30 - 18:45", IsArchived = false },
				new TimeBlock { TimeBlockValue = "17:30 - 19:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "17:30 - 20:10", IsArchived = false },
				new TimeBlock { TimeBlockValue = "19:30 - 20:45", IsArchived = false },
				new TimeBlock { TimeBlockValue = "07:30 - 09:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "09:30 - 10:45", IsArchived = false },
				new TimeBlock { TimeBlockValue = "12:00 - 13:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "10:30 - 12:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "08:00 - 09:15", IsArchived = false },
				new TimeBlock { TimeBlockValue = "08:30 - 10:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "11:30 - 13:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "19:30 - 21:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "07:40 - 08:40", IsArchived = false },
				new TimeBlock { TimeBlockValue = "11:30 - 12:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "12:30 - 13:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "13:00 - 13:50", IsArchived = false },
				new TimeBlock { TimeBlockValue = "19:00 - 20:15", IsArchived = false },
				new TimeBlock { TimeBlockValue = "16:30 - 17:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "09:00 - 10:15", IsArchived = false },
				new TimeBlock { TimeBlockValue = "10:30 - 11:45", IsArchived = false },
				new TimeBlock { TimeBlockValue = "18:00 - 19:15", IsArchived = false },
				new TimeBlock { TimeBlockValue = "14:30 - 16:20", IsArchived = false },
				new TimeBlock { TimeBlockValue = "19:00 - 19:50", IsArchived = false },
				new TimeBlock { TimeBlockValue = "13:30 - 14:45", IsArchived = false }
			};

			foreach (var t in TimeBlocks)
			{
				_db.TimeBlocks.Add(t);
			}
			_db.SaveChanges();

			//****************************************************************************** TimeBlocks

			// Seed the Semesters
			// - SemesterName
			// - IsArchived

			var Semesters = new List<Semester>
			{
				new Semester { SemesterName = "Fall", IsArchived = false },
				new Semester { SemesterName = "Spring", IsArchived = false },
				new Semester { SemesterName = "Summer", IsArchived = false }
			};

			foreach (var s in Semesters)
			{
				_db.Semesters.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** Semesters

			// Seed the SemestersInstances
			// - SemesterInstanceName
			// - StartDate
			// - EndDate
			// - RegistrationDate
			// - EndRegistrationDate
			// - SemesterId (FK)
			// - IsArchived

			var SemesterInstances = new List<SemesterInstance>
			{
				new SemesterInstance
				{
					SemesterInstanceName = "Spring 2024",
					StartDate = new DateTime(2024, 1, 10),
					EndDate = new DateTime(2024, 4, 29),
					RegistrationDate = new DateTime(2024, 10, 1),
					EndRegistrationDate = new DateTime(2024, 1, 9),
					SemesterId = 2,
					IsArchived = false
				},
				new SemesterInstance
				{
					SemesterInstanceName = "Summer 2024",
					StartDate = new DateTime(2024, 5, 9),
					EndDate = new DateTime(2024, 8, 5),
					RegistrationDate = new DateTime(2024, 1, 1),
					EndRegistrationDate = new DateTime(2024, 5, 8),
					SemesterId = 3,
					IsArchived = false
				},
				new SemesterInstance {
					SemesterInstanceName = "Fall 2024",
					StartDate = new DateTime(2024, 8, 23),
					EndDate = new DateTime(2024, 12, 10),
					RegistrationDate = new DateTime(2024, 4, 1),
					EndRegistrationDate = new DateTime(2024, 8, 22),
					SemesterId = 1,
					IsArchived = false
				}
			};

			foreach (var s in SemesterInstances)
			{
				_db.SemesterInstances.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** SemestersInstances

			// Seed the Modalities
			// - ModalityName
			// - IsArchived

			var Modalities = new List<Modality>
			{
				new Modality { ModalityName = "Face to Face", IsArchived = false },
				new Modality { ModalityName = "Online", IsArchived = false },
				new Modality { ModalityName = "Hybrid", IsArchived = false },
				new Modality { ModalityName = "CS Flex", IsArchived = false },
				new Modality { ModalityName = "WSU Flex", IsArchived = false },
				new Modality { ModalityName = "Release", IsArchived = false },
				new Modality { ModalityName = "SWI", IsArchived = false },
				new Modality { ModalityName = "Virtual", IsArchived = false },
				new Modality { ModalityName = "Virtual Hybrid", IsArchived = false },
				new Modality { ModalityName = "TBD", IsArchived = false },
				new Modality { ModalityName = "Stipend", IsArchived = false }
			};

			foreach (var m in Modalities)
			{
				_db.Modalities.Add(m);
			}
			_db.SaveChanges();

			//****************************************************************************** Modalities

			// Seed the CourseSections
			// - BannerCRN
			// - SectionNotes
			// - SectionFirstDayEnrollment
			// - SectionFinalEnrollment
			// - SectionUpdated
			// - SectionBannerUpdated
			// - CourseId (FK)
			// - SemesterInstanceId (FK)
			// - InstructorId (FK)
			// - ModalityId (FK)
			// - ClassroomId (FK)
			// - TimeBlockId (FK)
			// - DaysOfWeekId (FK)
			// - PartOfTermId (FK)
			// - PayModelId (FK)
			// - PayOrganizationId (FK)
			// - SectionStatusId (FK)
			// - IsArchived

			var CourseSections = new List<CourseSection>
			{
				new CourseSection
				{
					BannerCRN = "12345",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 1,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 1,
					TimeBlockId = 1,
					DaysOfWeekId = 1,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "12346",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 1,
					InstructorId = instr2.Id,
					ModalityId = 3,
					ClassroomId = 2,
					TimeBlockId = 7,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "12347",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 1,
					InstructorId = instr4.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 4,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "12348",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 1,
					InstructorId = instr5.Id,
					ModalityId = 1,
					ClassroomId = 1,
					TimeBlockId = 2,
					DaysOfWeekId = 1,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "23456",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 2,
					SemesterInstanceId = 1,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 2,
					TimeBlockId = 2,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "23457",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 2,
					SemesterInstanceId = 1,
					InstructorId = instr3.Id,
					ModalityId = 4,
					ClassroomId = 7,
					TimeBlockId = 19,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "23458",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 2,
					SemesterInstanceId = 1,
					InstructorId = instr.Id,
					ModalityId = 3,
					ClassroomId = 8,
					TimeBlockId = 13,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "23459",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 2,
					SemesterInstanceId = 1,
					InstructorId = instr5.Id,
					ModalityId = 1,
					ClassroomId = 4,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "34567",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 3,
					SemesterInstanceId = 1,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 3,
					TimeBlockId = 3,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "34568",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 3,
					SemesterInstanceId = 1,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 3,
					TimeBlockId = 10,
					DaysOfWeekId = 1,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "34569",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 112,
					SemesterInstanceId = 2,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 3,
					TimeBlockId = 3,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "34570",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 112,
					SemesterInstanceId = 2,
					InstructorId = instr3.Id,
					ModalityId = 2,
					ClassroomId = 8,
					TimeBlockId = 6,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "34571",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 112,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 3,
					TimeBlockId = 9,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "45678",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 63,
					SemesterInstanceId = 2,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 4,
					TimeBlockId = 4,
					DaysOfWeekId = 4,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "45678",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 63,
					SemesterInstanceId = 2,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 4,
					TimeBlockId = 4,
					DaysOfWeekId = 4,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "56789",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 14,
					SemesterInstanceId = 2,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 12,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "56789",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 14,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 7,
					TimeBlockId = 14,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "78965",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 7,
					SemesterInstanceId = 3,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 5,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "78964",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 7,
					SemesterInstanceId = 3,
					InstructorId = instr5.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 6,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "89654",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 19,
					SemesterInstanceId = 3,
					InstructorId = instr2.Id,
					ModalityId = 3,
					ClassroomId = 5,
					TimeBlockId = 2,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "89653",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 19,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 3,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "89652",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 19,
					SemesterInstanceId = 3,
					InstructorId = instr3.Id,
					ModalityId = 3,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "96543",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 99,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 4,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "96542",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 99,
					SemesterInstanceId = 3,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 5,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "96541",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 99,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 5,
					ClassroomId = 5,
					TimeBlockId = 15,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "65432",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 4,
					SemesterInstanceId = 1,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "65431",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 4,
					SemesterInstanceId = 1,
					InstructorId = instr2.Id,
					ModalityId = 3,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "65430",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 4,
					SemesterInstanceId = 1,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "54321",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 5,
					SemesterInstanceId = 1,
					InstructorId = instr3.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "54320",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 5,
					SemesterInstanceId = 1,
					InstructorId = instr4.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 3,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "43210",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 6,
					SemesterInstanceId = 1,
					InstructorId = instr.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "43219",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 6,
					SemesterInstanceId = 1,
					InstructorId = instr2.Id,
					ModalityId = 5,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "32109",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 13,
					SemesterInstanceId = 2,
					InstructorId = instr3.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 9,
					DaysOfWeekId = 5,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "32100",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 13,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 3,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "32101",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 13,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 5,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "21098",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 21,
					SemesterInstanceId = 2,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 4,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21097",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 21,
					SemesterInstanceId = 2,
					InstructorId = instr.Id,
					ModalityId = 2,
					ClassroomId = 5,
					TimeBlockId = 4,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21096",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 21,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 3,
					ClassroomId = 5,
					TimeBlockId = 5,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "21087",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 69,
					SemesterInstanceId = 2,
					InstructorId = instr.Id,
					ModalityId = 4,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21086",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 69,
					SemesterInstanceId = 2,
					InstructorId = instr4.Id,
					ModalityId = 4,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21085",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 69,
					SemesterInstanceId = 2,
					InstructorId = instr5.Id,
					ModalityId = 4,
					ClassroomId = 5,
					TimeBlockId = 8,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "21076",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 10,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21075",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 3,
					InstructorId = instr3.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 2,
					DaysOfWeekId = 4,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21074",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 1,
					SemesterInstanceId = 3,
					InstructorId = instr2.Id,
					ModalityId = 1,
					ClassroomId = 5,
					TimeBlockId = 11,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "21065",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 5,
					SemesterInstanceId = 3,
					InstructorId = instr2.Id,
					ModalityId = 7,
					ClassroomId = 5,
					TimeBlockId = 2,
					DaysOfWeekId = 4,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21064",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 5,
					SemesterInstanceId = 3,
					InstructorId = instr4.Id,
					ModalityId = 7,
					ClassroomId = 5,
					TimeBlockId = 3,
					DaysOfWeekId = 1,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21063",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 5,
					SemesterInstanceId = 3,
					InstructorId = instr5.Id,
					ModalityId = 3,
					ClassroomId = 5,
					TimeBlockId = 2,
					DaysOfWeekId = 4,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},

				new CourseSection
				{
					BannerCRN = "21054",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 9,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 1,
					ClassroomId = 4,
					TimeBlockId = 5,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21053",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 9,
					SemesterInstanceId = 3,
					InstructorId = instr.Id,
					ModalityId = 2,
					ClassroomId = 4,
					TimeBlockId = 5,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				},
				new CourseSection
				{
					BannerCRN = "21052",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2024, 6, 23),
					SectionFinalEnrollment = new DateTime(2024, 8, 23),
					SectionUpdated = new DateTime(2024, 8, 23),
					SectionBannerUpdated = new DateTime(2024, 8, 23),
					CourseId = 9,
					SemesterInstanceId = 3,
					InstructorId = instr3.Id,
					ModalityId = 3,
					ClassroomId = 4,
					TimeBlockId = 2,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1,
					IsArchived = false
				} 
			};

			foreach (var c in CourseSections)
			{
				_db.CourseSections.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** CourseSections

			// Seed the ReleaseTimes
			// - ReleaseTimeAmount
			// - ReleaseTimeNotes
			// - SemesterInstanceId (FK)
			// - InstructorId (FK)
			// - IsArchived

			var ReleaseTimes = new List<ReleaseTime>
			{
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterInstanceId = 1, InstructorId = instr.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 4, ReleaseTimeNotes = "None", SemesterInstanceId = 2, InstructorId = instr.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 4, ReleaseTimeNotes = "None", SemesterInstanceId = 3, InstructorId = instr.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 2, ReleaseTimeNotes = "None", SemesterInstanceId = 1, InstructorId = instr2.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 1, ReleaseTimeNotes = "None", SemesterInstanceId = 2, InstructorId = instr2.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 2, ReleaseTimeNotes = "None", SemesterInstanceId = 3, InstructorId = instr2.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 5, ReleaseTimeNotes = "None", SemesterInstanceId = 1, InstructorId = instr3.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterInstanceId = 2, InstructorId = instr3.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterInstanceId = 3, InstructorId = instr3.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 2, ReleaseTimeNotes = "None", SemesterInstanceId = 1, InstructorId = instr4.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 1, ReleaseTimeNotes = "None", SemesterInstanceId = 2, InstructorId = instr4.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 1, ReleaseTimeNotes = "None", SemesterInstanceId = 3, InstructorId = instr4.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 0, ReleaseTimeNotes = "None", SemesterInstanceId = 1, InstructorId = instr5.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterInstanceId = 2, InstructorId = instr5.Id, IsArchived = false },
				new ReleaseTime { ReleaseTimeAmount = 2, ReleaseTimeNotes = "None", SemesterInstanceId = 3, InstructorId = instr5.Id, IsArchived = false }
			};

			foreach (var r in ReleaseTimes)
			{
				_db.ReleaseTimes.Add(r);
			}
			_db.SaveChanges();

			//****************************************************************************** ReleaseTimes

			// Seed the LoadReqs
			// - LoadReqHours
			// - InstructorId (FK)
			// - SemesterId (FK)
			// - IsArchived

			var LoadReqs = new List<LoadReq>
			{
				new LoadReq { LoadReqAmount = 12, InstructorId = instr.Id, SemesterInstanceId = 1, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr.Id, SemesterInstanceId = 2, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr.Id, SemesterInstanceId = 3, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr2.Id, SemesterInstanceId = 1, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr2.Id, SemesterInstanceId = 2, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr2.Id, SemesterInstanceId = 3, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr3.Id, SemesterInstanceId = 1, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr3.Id, SemesterInstanceId = 2, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr3.Id, SemesterInstanceId = 3, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr4.Id, SemesterInstanceId = 1, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr4.Id, SemesterInstanceId = 2, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr4.Id, SemesterInstanceId = 3, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr5.Id, SemesterInstanceId = 1, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr5.Id, SemesterInstanceId = 2, IsArchived = false },
				new LoadReq { LoadReqAmount = 12, InstructorId = instr5.Id, SemesterInstanceId = 3, IsArchived = false }
			};

			foreach (var l in LoadReqs)
			{
				_db.LoadReqs.Add(l);
			}
			_db.SaveChanges();

			//****************************************************************************** LoadReqs

			// Seed the Templates
			// - Quantity
			// - SemesterId (FK)
			// - CourseId (FK)
			// - IsArchived

			var Templates = new List<Template>
			{
				new Template { Quantity = 4, SemesterId = 1, CourseId = 1, IsArchived = false },
				new Template { Quantity = 4, SemesterId = 1, CourseId = 2, IsArchived = false },
				new Template { Quantity = 2, SemesterId = 1, CourseId = 3, IsArchived = false },

				new Template { Quantity = 3, SemesterId = 2, CourseId = 112, IsArchived = false },
				new Template { Quantity = 2, SemesterId = 2, CourseId = 63, IsArchived = false },
				new Template { Quantity = 2, SemesterId = 2, CourseId = 14, IsArchived = false },

				new Template { Quantity = 2, SemesterId = 3, CourseId = 7, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 3, CourseId = 19, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 3, CourseId = 99, IsArchived = false },

				new Template { Quantity = 3, SemesterId = 1, CourseId = 4, IsArchived = false },
				new Template { Quantity = 2, SemesterId = 1, CourseId = 5, IsArchived = false },
				new Template { Quantity = 2, SemesterId = 1, CourseId = 6, IsArchived = false },

				new Template { Quantity = 3, SemesterId = 2, CourseId = 13, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 2, CourseId = 21, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 2, CourseId = 69, IsArchived = false },

				new Template { Quantity = 3, SemesterId = 3, CourseId = 1, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 3, CourseId = 5, IsArchived = false },
				new Template { Quantity = 3, SemesterId = 3, CourseId = 9, IsArchived = false }
			};

			foreach (var t in Templates)
			{
				_db.Templates.Add(t);
			}
			_db.SaveChanges();

			//****************************************************************************** Templates

			// Seed the Wishlist
			// - UserId (FK)
			// - SemesterInstanceId (FK)
			// - IsArchived

			var Wishlists = new List<Wishlist>
			{
				new Wishlist { UserId = instr.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = instr.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = instr.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = stud.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = stud.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = stud.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = instr2.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = instr2.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = instr2.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = stud2.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = stud2.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = stud2.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = instr3.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = instr3.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = instr3.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = stud3.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = stud3.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = stud3.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = instr4.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = instr4.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = instr4.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = stud4.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = stud4.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = stud4.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = instr5.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = instr5.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = instr5.Id, SemesterInstanceId = 3, IsArchived = false },
				new Wishlist { UserId = stud5.Id, SemesterInstanceId = 1, IsArchived = false },
				new Wishlist { UserId = stud5.Id, SemesterInstanceId = 2, IsArchived = false },
				new Wishlist { UserId = stud5.Id, SemesterInstanceId = 3, IsArchived = false }

			};

			foreach (var w in Wishlists)
			{
				_db.Wishlists.Add(w);
			}
			_db.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (int j = 1; j <= 3; j++)
                {
                    Wishlist wishlist = new Wishlist
                    {
                        UserId = studentUser.Id,
                        SemesterInstanceId = j,
                        IsArchived = false
                    };
                    _db.Wishlists.Add(wishlist);
                }
            }
            _db.SaveChanges();


            //****************************************************************************** Wishlists

            // Seed the WishlistCourse
            // - PreferenceRank (nullable)
            // - WishlistId (FK)
            // - CourseId (FK)
            // - IsArchived

            var WishlistCourses = new List<WishlistCourse>
			{
				new WishlistCourse { PreferenceRank = 1, WishlistId = 1, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 1, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 1, CourseId = 5, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 4, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 4, CourseId = 3, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 4, CourseId = 6, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 7, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 7, CourseId = 3, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 7, CourseId = 4, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 10, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 10, CourseId = 4, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 10, CourseId = 6, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 13, CourseId = 3, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 13, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 13, CourseId = 4, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 16, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 16, CourseId = 5, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 16, CourseId = 6, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 19, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 19, CourseId = 4, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 19, CourseId = 3, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 22, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 22, CourseId = 3, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 22, CourseId = 4, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 25, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 25, CourseId = 5, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 25, CourseId = 6, IsArchived = false },
				new WishlistCourse { PreferenceRank = 1, WishlistId = 28, CourseId = 1, IsArchived = false },
				new WishlistCourse { PreferenceRank = 2, WishlistId = 28, CourseId = 2, IsArchived = false },
				new WishlistCourse { PreferenceRank = 3, WishlistId = 28, CourseId = 3, IsArchived = false }
			};

			foreach (var w in WishlistCourses)
			{
				_db.WishlistCourses.Add(w);
			}
			_db.SaveChanges();

            int[][] courseIds = new int[][]
			{
				new int[] {1, 2, 3, 4, 5, 6}, // CourseId's for SemesterInstanceId 1
				new int[] {112, 63, 14, 13, 21, 69}, // CourseId's for SemesterInstanceId 2
				new int[] {7, 19, 99, 1, 5, 9} // CourseId's for SemesterInstanceId 3
			};

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (int j = 0; j < 3; j++)
                {
                    Wishlist wishlist = _db.Wishlists.FirstOrDefault(w => w.UserId == studentUser.Id && w.SemesterInstanceId == j + 1);

                    for (int k = 0; k < courseIds[j].Length; k++)
                    {
                        WishlistCourse wishlistCourse = new WishlistCourse
                        {
                            PreferenceRank = k + 1,
                            WishlistId = wishlist.Id,
                            CourseId = courseIds[j][k],
                            IsArchived = false
                        };
                        _db.WishlistCourses.Add(wishlistCourse);
                    }
                }
            }
            _db.SaveChanges();


            //****************************************************************************** WishlistCourses
            // Seed the PartOfDay
            // - PartOfDay (FK)
            // - isArchived

            var PartOfDay = new List<PartOfDay>
			{
				new PartOfDay { PartOfDayValue = "Morning", IsArchived = false },
				new PartOfDay { PartOfDayValue = "Afternoon", IsArchived = false },
				new PartOfDay { PartOfDayValue = "Evening", IsArchived = false }
			};

			foreach (var t in PartOfDay)
			{
				_db.PartOfDays.Add(t);
			}
			_db.SaveChanges();

			//****************************************************************************** PartOfDay
			//Seed WishlistPartOfDay
			// - WishlistId (FK)
			// - PartOfDayId (FK)
			// - IsArchived

			var WishlistPartOfDay = new List<WishlistPartOfDay>
			{
				new WishlistPartOfDay { WishlistId = 10, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 11, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 12, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 12, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 4, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 5, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 5, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 6, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 16, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 17, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 18, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 18, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 22, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 23, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 24, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 24, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 28, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 29, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 30, PartOfDayId = 2, IsArchived = false }
			};

			foreach (var w in WishlistPartOfDay)
			{
                _db.WishlistPartOfDays.Add(w);
            }
			_db.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (int j = 0; j < 3; j++)
                {
                    Wishlist wishlist = _db.Wishlists.FirstOrDefault(w => w.UserId == studentUser.Id && w.SemesterInstanceId == j + 1);

                    for (int k = 1; k <= 3; k++)
                    {
                        WishlistPartOfDay wishlistPartOfDay = new WishlistPartOfDay
                        {
                            WishlistId = wishlist.Id,
                            PartOfDayId = k,
                            IsArchived = false
                        };
                        _db.WishlistPartOfDays.Add(wishlistPartOfDay);
                    }
                }
            }
            _db.SaveChanges();

            //****************************************************************************** WishlistPartOfDay
            // Seed the WishlistCampus
            // - WishlistId (FK)
            // - CampusId (FK)
            // - IsArchived

            var WishlistCampuses = new List<WishlistCampus>
			{
				new WishlistCampus { WishlistId = 1, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 1, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 2, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 3, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 4, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 5, CampusId = 4, IsArchived = false },
				new WishlistCampus { WishlistId = 6, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 6, CampusId = 5, IsArchived = false },
				new WishlistCampus { WishlistId = 7, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 8, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 9, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 10, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 11, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 12, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 13, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 14, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 15, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 16, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 17, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 18, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 19, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 20, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 21, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 22, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 23, CampusId = 1, IsArchived = false },
				new WishlistCampus { WishlistId = 24, CampusId = 4, IsArchived = false },
				new WishlistCampus { WishlistId = 25, CampusId = 5, IsArchived = false },
				new WishlistCampus { WishlistId = 26, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 27, CampusId = 2, IsArchived = false },
				new WishlistCampus { WishlistId = 28, CampusId = 4, IsArchived = false },
				new WishlistCampus { WishlistId = 29, CampusId = 3, IsArchived = false },
				new WishlistCampus { WishlistId = 30, CampusId = 3, IsArchived = false }
			};

			foreach (var w in WishlistCampuses)
			{
				_db.WishlistCampuses.Add(w);
			}
			_db.SaveChanges();

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (int j = 0; j < 3; j++)
                {
                    Wishlist wishlist = _db.Wishlists.FirstOrDefault(w => w.UserId == studentUser.Id && w.SemesterInstanceId == j + 1);

                    // Generate a random number of campuses for each user (between 1 and 5)
                    int numCampuses = random.Next(1, 6);

                    for (int k = 0; k < numCampuses; k++)
                    {
                        // Generate a random CampusId for each WishlistCampus (between 1 and 5)
                        int campusId = random.Next(1, 6);

                        WishlistCampus wishlistCampus = new WishlistCampus
                        {
                            WishlistId = wishlist.Id,
                            CampusId = campusId,
                            IsArchived = false
                        };
                        _db.WishlistCampuses.Add(wishlistCampus);
                    }
                }
            }
            _db.SaveChanges();


            //****************************************************************************** WishlistCampuses
            // Seed the WishlistDaysOfWeek
            // - WishlistId (FK)
            // - DaysOfWeekId (FK)
            // - IsArchived

            var WishlistDaysOfWeeks = new List<WishlistDaysOfWeek>
			{
				new WishlistDaysOfWeek { WishlistId = 1, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 1, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 2, DaysOfWeekId = 2, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 2, DaysOfWeekId = 4, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 3, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 3, DaysOfWeekId = 2, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 7, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 7, DaysOfWeekId = 4, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 8, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 8, DaysOfWeekId = 2, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 9, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 9, DaysOfWeekId = 4, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 13, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 13, DaysOfWeekId = 2, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 14, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 14, DaysOfWeekId = 4, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 15, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 15, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 15, DaysOfWeekId = 5, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 19, DaysOfWeekId = 2, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 20, DaysOfWeekId = 4, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 21, DaysOfWeekId = 1, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 25, DaysOfWeekId = 3, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 26, DaysOfWeekId = 5, IsArchived = false },
				new WishlistDaysOfWeek { WishlistId = 27, DaysOfWeekId = 2, IsArchived = false }
			};

			foreach (var w in WishlistDaysOfWeeks)
			{
				_db.WishlistDaysOfWeeks.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** WishlistDaysOfWeeks
			// Seed the WishlistModality
			// - WishlistId (FK)
			// - ModalityId (FK)
			// - IsArchived

			var WishlistModalities = new List<WishlistModality>
			{
				new WishlistModality { WishlistId = 1, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 1, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 2, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 3, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 3, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 3, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 4, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 4, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 5, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 5, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 6, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 6, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 7, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 7, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 8, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 8, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 9, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 9, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 10, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 10, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 11, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 11, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 12, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 12, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 13, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 13, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 14, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 14, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 15, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 15, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 16, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 16, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 17, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 17, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 18, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 18, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 19, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 19, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 20, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 20, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 21, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 21, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 22, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 22, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 23, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 23, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 24, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 24, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 25, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 25, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 26, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 26, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 27, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 27, ModalityId = 6, IsArchived = false },
				new WishlistModality { WishlistId = 28, ModalityId = 1, IsArchived = false },
				new WishlistModality { WishlistId = 28, ModalityId = 2, IsArchived = false },
				new WishlistModality { WishlistId = 29, ModalityId = 3, IsArchived = false },
				new WishlistModality { WishlistId = 29, ModalityId = 4, IsArchived = false },
				new WishlistModality { WishlistId = 30, ModalityId = 5, IsArchived = false },
				new WishlistModality { WishlistId = 30, ModalityId = 6, IsArchived = false }
			};

			foreach (var w in WishlistModalities)
			{
				_db.WishlistModalities.Add(w);
			}
			_db.SaveChanges();

            //Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                string email = $"student{6 + i}@student.com";
                ApplicationUser studentUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (int j = 0; j < 3; j++)
                {
                    Wishlist wishlist = _db.Wishlists.FirstOrDefault(w => w.UserId == studentUser.Id && w.SemesterInstanceId == j + 1);

                    // Generate a random number of modalities for each user (between 1 and 11)
                    int numModalities = random.Next(1, 12);

                    for (int k = 0; k < numModalities; k++)
                    {
                        // Generate a random ModalityId for each WishlistModality (between 1 and 11)
                        int modalityId = random.Next(1, 12);

                        WishlistModality wishlistModality = new WishlistModality
                        {
                            WishlistId = wishlist.Id,
                            ModalityId = modalityId,
                            IsArchived = false
                        };
                        _db.WishlistModalities.Add(wishlistModality);
                    }
                }
            }
            _db.SaveChanges();


            //****************************************************************************** WishlistModalities
            // Seed the WishlistPartOfDays
            // - WishlistId (FK)
            // - PartOfDayId (FK)
            // - IsArchived

            var WishlistPartOfDays = new List<WishlistPartOfDay>
			{
				new WishlistPartOfDay { WishlistId = 4, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 4, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 5, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 6, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 10, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 11, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 11, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 12, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 16, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 17, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 18, PartOfDayId = 3, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 22, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 22, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 23, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 24, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 28, PartOfDayId = 1, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 29, PartOfDayId = 2, IsArchived = false },
				new WishlistPartOfDay { WishlistId = 30, PartOfDayId = 3, IsArchived = false }
			};

			foreach (var w in WishlistPartOfDays)
			{
				_db.WishlistPartOfDays.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** WishlistPartOfDays
			// Seed the WishlistTimeBlocks
			// - WishlistId (FK)
			// - TimeBlockId (FK)
			// - IsArchived

			var WishlistTimeBlocks = new List<WishlistTimeBlock>
			{
				new WishlistTimeBlock { WishlistId = 1, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 1, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 1, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 2, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 2, TimeBlockId = 4, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 2, TimeBlockId = 12, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 5, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 11, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 12, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 3, TimeBlockId = 13, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 7, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 7, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 7, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 8, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 8, TimeBlockId = 4, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 8, TimeBlockId = 6, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 9, TimeBlockId = 11, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 9, TimeBlockId = 12, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 9, TimeBlockId = 13, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 13, TimeBlockId = 9, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 13, TimeBlockId = 10, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 13, TimeBlockId = 11, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 14, TimeBlockId = 6, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 14, TimeBlockId = 7, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 14, TimeBlockId = 8, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 15, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 15, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 15, TimeBlockId = 5, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 19, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 19, TimeBlockId = 4, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 19, TimeBlockId = 6, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 20, TimeBlockId = 9, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 20, TimeBlockId = 10, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 20, TimeBlockId = 11, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 21, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 21, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 21, TimeBlockId = 5, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 25, TimeBlockId = 2, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 25, TimeBlockId = 4, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 25, TimeBlockId = 6, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 26, TimeBlockId = 9, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 26, TimeBlockId = 10, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 26, TimeBlockId = 11, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 27, TimeBlockId = 1, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 27, TimeBlockId = 3, IsArchived = false },
				new WishlistTimeBlock { WishlistId = 27, TimeBlockId = 5, IsArchived = false }

			};

			foreach (var w in WishlistTimeBlocks)
			{
				_db.WishlistTimeBlocks.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** WishlistTimeBlocks
		}
	}
}

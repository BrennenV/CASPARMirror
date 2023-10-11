using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;

		public DbInitializer(ApplicationDbContext db)
		{
			_db = db;
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


			// Seed the Users
			// - UserFirstName
			// - UserLastName
			// - UserEmail
			// - UserPassword

			if (_db.Users.Any())
			{
				return; //DB has been seeded
			}

			var Users = new List<User>
			{
				new User { UserFirstName = "Chris", UserLastName = "Jensen", UserEmail = "chrisjensen3@mail.weber.edu", UserPassword = "password"},
				new User { UserFirstName = "Joseph", UserLastName = "Brower", UserEmail = "josephbrower@mail.weber.edu", UserPassword = "wordpass"},
				new User { UserFirstName = "Jaeden", UserLastName = "Fisher", UserEmail = "jaedenfisher@mail.weber.edu", UserPassword = "pass"},
				new User { UserFirstName = "Brennen", UserLastName = "Vanderpool", UserEmail = "brennenvanderpool@mail.weber.edu", UserPassword = "word"},
				new User { UserFirstName = "Stacey", UserLastName = "Smom", UserEmail = "hasgotitgoingon@yahoo.com", UserPassword = "password1"},
				new User { UserFirstName = "Richard", UserLastName = "Fry", UserEmail = "rfry@weber.edu", UserPassword = "temp1234"}
			};

			foreach (var u in Users)
			{
				_db.Users.Add(u);
			}
			_db.SaveChanges();

			//****************************************************************************** Users

			// Seed the Roles
			// - RoleName

			var Roles = new List<Role>
			{
				new Role { RoleName = "Admin" },
				new Role { RoleName = "Instructor" },
				new Role { RoleName = "Student" }
			};

			foreach (var r in Roles)
			{
				_db.Roles.Add(r);
			}
			_db.SaveChanges();

			//****************************************************************************** Roles

			// Seed the RoleAssignments
			// - RoleId (FK)
			// - UserId (FK)

			var RoleAssignments = new List<RoleAssignment>
			{
				new RoleAssignment { RoleId = 1, UserId = 1 },
				new RoleAssignment { RoleId = 2, UserId = 5 },
				new RoleAssignment { RoleId = 2, UserId = 6 },
				new RoleAssignment { RoleId = 3, UserId = 2 },
				new RoleAssignment { RoleId = 3, UserId = 3 },
				new RoleAssignment { RoleId = 3, UserId = 4 }
			};

			foreach (var r in RoleAssignments)
			{
				_db.RoleAssignments.Add(r);
			}
			_db.SaveChanges();

			//****************************************************************************** RoleAssignments

			// Seed the Instructors
			// - InstructorName
			// - UserId (FK)

			var Instructors = new List<Instructor>
			{ 
				new Instructor { InstructorName = "Stacey Smom", UserId = 5 },
				new Instructor { InstructorName = "Richard Fry", UserId = 6 }
			};

			foreach (var i in Instructors)
			{
				_db.Instructors.Add(i);
			}
			_db.SaveChanges();

			//****************************************************************************** Instructors

			// Seed the Students
			// - StudentMajor
			// - StudentGradYear
			// - UserId (FK)

			var Students = new List<Student>
			{
				new Student { StudentMajor = "Computer Science", StudentGradYear = "2023", UserId = 1 },
				new Student { StudentMajor = "Liberal Arts", StudentGradYear = "2024", UserId = 2 },
				new Student { StudentMajor = "Political Science", StudentGradYear = "2024", UserId = 3 },
				new Student { StudentMajor = "Basket Weaving", StudentGradYear = "2025", UserId = 4 }
			};

			foreach (var s in Students)
			{
				_db.Students.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** Students

			// Seed the Campuses
			// - CampusName

			var Campuses = new List<Campus>
			{
				new Campus { CampusName = "Ogden" },
				new Campus { CampusName = "Davis"},
				new Campus { CampusName = "Salt Lake City" },
				new Campus { CampusName = "Farmington" }
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

			var Buildings = new List<Building>
			{
				new Building { BuildingName = "Elizabeth Hall", CampusId = 1 },
				new Building { BuildingName = "Tracy Hall Science Center", CampusId = 1 },
				new Building { BuildingName = "Lindquist Hall", CampusId = 1 },
				new Building { BuildingName = "Browning Center", CampusId = 1 },
				new Building { BuildingName = "Davis Building", CampusId = 2 },
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

			var Classrooms = new List<Classroom>
			{
				new Classroom { BuildingId = 1, ClassroomDetail = "Classroom 1", ClassroomSeats = 30, ClassroomNumber = "EH 101" },
				new Classroom { BuildingId = 1, ClassroomDetail = "Classroom 2", ClassroomSeats = 30, ClassroomNumber = "EH 232" },
				new Classroom { BuildingId = 2, ClassroomDetail = "Classroom 3", ClassroomSeats = 30, ClassroomNumber = "TH 308" },
				new Classroom { BuildingId = 3, ClassroomDetail = "Classroom 4", ClassroomSeats = 70, ClassroomNumber = "LH 119" },
				new Classroom { BuildingId = 4, ClassroomDetail = "Classroom 5", ClassroomSeats = 70, ClassroomNumber = "BC 104" },
				new Classroom { BuildingId = 5, ClassroomDetail = "Classroom 6", ClassroomSeats = 40, ClassroomNumber = "DB 201" },
			};

			foreach (var c in Classrooms)
			{
				_db.Classrooms.Add(c);
			}
			_db.SaveChanges();

			//****************************************************************************** Classrooms

			// Seed the ClassroomAmenities
			// - ClassroomAmenityName

			var ClassroomAmenities = new List<ClassroomAmenity>
			{
				new ClassroomAmenity { ClassroomAmenityName = "Computer" },
				new ClassroomAmenity { ClassroomAmenityName = "Multiple Projectors" },
				new ClassroomAmenity { ClassroomAmenityName = "Multiple Whiteboards" },
				new ClassroomAmenity { ClassroomAmenityName = "Lab Stations" },
				new ClassroomAmenity { ClassroomAmenityName = "Auditorium" }
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

			var ClassroomAmenityPossessions = new List<ClassroomAmenityPossession>
			{
				new ClassroomAmenityPossession { ClassroomAmenityId = 1, ClassroomId = 1 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 1 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 2 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 4, ClassroomId = 3 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 5, ClassroomId = 4 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 3, ClassroomId = 4 },
				new ClassroomAmenityPossession { ClassroomAmenityId = 2, ClassroomId = 4 }
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

			var AcademicPrograms = new List<AcademicProgram>
			{ 
				new AcademicProgram { ProgramName = "Computer Science", ProgramCode = "CS" },
				new AcademicProgram { ProgramName = "English", ProgramCode = "ENG" },
				new AcademicProgram { ProgramName = "Political Science", ProgramCode = "POLS" },
				new AcademicProgram { ProgramName = "History", ProgramCode = "HIST" },
				new AcademicProgram { ProgramName = "Mathematics", ProgramCode = "MATH" }
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

			var Courses = new List<Course>
			{
				new Course { CourseTitle = "Introduction to Computer Science", CourseCreditHours = 3, CourseNumber = "CS 1030", CourseDescription = "Introduction to computer science", CourseNotes = "None", ProgramId = 1 },
				new Course { CourseTitle = "Introduction to Programming", CourseCreditHours = 3, CourseNumber = "CS 1400", CourseDescription = "Introduction to programming", CourseNotes = "None", ProgramId = 1 },
				new Course { CourseTitle = "Introduction to Literature", CourseCreditHours = 3, CourseNumber = "ENG 1010", CourseDescription = "Introduction to literature", CourseNotes = "None", ProgramId = 2 },
				new Course { CourseTitle = "Introduction to Political Science", CourseCreditHours = 3, CourseNumber = "POLS 1010", CourseDescription = "Introduction to political science", CourseNotes = "None", ProgramId = 3 },
				new Course { CourseTitle = "Introduction to History", CourseCreditHours = 3, CourseNumber = "HIST 1010", CourseDescription = "Introduction to history", CourseNotes = "None", ProgramId = 4 },
				new Course { CourseTitle = "Introduction to Statistics", CourseCreditHours = 3, CourseNumber = "MATH 1080", CourseDescription = "Introduction to statistics", CourseNotes = "None", ProgramId = 5 }
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

			var ProgramAssignments = new List<ProgramAssignment>
			{
				new ProgramAssignment { InstructorId = 1, ProgramId = 4 },
				new ProgramAssignment { InstructorId = 1, ProgramId = 2 },
				new ProgramAssignment { InstructorId = 2, ProgramId = 1 },
				new ProgramAssignment { InstructorId = 2, ProgramId = 3 },
				new ProgramAssignment { InstructorId = 2, ProgramId = 5 }
			};

			foreach (var p in ProgramAssignments)
			{
				_db.ProgramAssignments.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** ProgramAssignments

			// Seed the PartOfTerms
			// - PartOfTermTitle

			var PartOfTerms = new List<PartOfTerm>
			{
				new PartOfTerm { PartOfTermTitle = "Full Semester" },
				new PartOfTerm { PartOfTermTitle = "First Half Semester" },
				new PartOfTerm { PartOfTermTitle = "Second Half Semester" },
				new PartOfTerm { PartOfTermTitle = "First 7 Weeks" },
				new PartOfTerm { PartOfTermTitle = "Second 7 Weeks" }
			};

			foreach (var p in PartOfTerms)
			{
				_db.PartOfTerms.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PartOfTerms

			// Seed the PayModels
			// - PayModelTitle

			var PayModels = new List<PayModel>
			{
				new PayModel { PayModelTitle = "Per Course" },
				new PayModel { PayModelTitle = "Per Credit Hour" },
				new PayModel { PayModelTitle = "Per Semester" }
			};

			foreach (var p in PayModels)
			{
				_db.PayModels.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PayModels

			// Seed the PayOrganizations
			// - PayOrganizationTitle

			var PayOrganizations = new List<PayOrganization>
			{
				new PayOrganization { PayOrganizationTitle = "Weber State University" },
				new PayOrganization { PayOrganizationTitle = "Utah State University" },
				new PayOrganization { PayOrganizationTitle = "University of Utah" },
				new PayOrganization { PayOrganizationTitle = "Utah Valley University" }
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

			var SectionStatuses = new List<SectionStatus>
			{
				new SectionStatus { SectionStatusName = "Pending", SectionStatusColor = "Yellow" },
				new SectionStatus { SectionStatusName = "Approved", SectionStatusColor = "Green" },
				new SectionStatus { SectionStatusName = "Denied", SectionStatusColor = "Red" }
			};

			foreach (var s in SectionStatuses)
			{
				_db.SectionStatuses.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** SectionStatuses

			// Seed the DaysOfWeeks
			// - DaysOfWeekTitle

			var DaysOfWeeks = new List<DaysOfWeek>
			{
				new DaysOfWeek { DaysOfWeekTitle = "Monday" },
				new DaysOfWeek { DaysOfWeekTitle = "Tuesday" },
				new DaysOfWeek { DaysOfWeekTitle = "Wednesday" },
				new DaysOfWeek { DaysOfWeekTitle = "Thursday" },
				new DaysOfWeek { DaysOfWeekTitle = "Friday" },
				new DaysOfWeek { DaysOfWeekTitle = "Saturday" },
				new DaysOfWeek { DaysOfWeekTitle = "Sunday" }
			};

			foreach (var d in DaysOfWeeks)
			{
				_db.DaysOfWeeks.Add(d);
			}
			_db.SaveChanges();

			//****************************************************************************** DaysOfWeeks

			// Seed the TimeBlocks
			// - TimeBlockValue

			var TimeBlocks = new List<TimeBlock>
			{
				new TimeBlock { TimeBlockValue = "8:00 AM" },
				new TimeBlock { TimeBlockValue = "9:00 AM" },
				new TimeBlock { TimeBlockValue = "10:00 AM" },
				new TimeBlock { TimeBlockValue = "11:00 AM" },
				new TimeBlock { TimeBlockValue = "12:00 PM" },
				new TimeBlock { TimeBlockValue = "1:00 PM" },
				new TimeBlock { TimeBlockValue = "2:00 PM" },
				new TimeBlock { TimeBlockValue = "3:00 PM" },
				new TimeBlock { TimeBlockValue = "4:00 PM" },
				new TimeBlock { TimeBlockValue = "5:00 PM" },
				new TimeBlock { TimeBlockValue = "6:00 PM" }
			};

			foreach (var t in TimeBlocks)
			{
				_db.TimeBlocks.Add(t);
			}
			_db.SaveChanges();

			//****************************************************************************** TimeBlocks

			// Seed the Semesters
			// - SemesterName

			var Semesters = new List<Semester>
			{
				new Semester { SemesterName = "Fall" },
				new Semester { SemesterName = "Spring" },
				new Semester { SemesterName = "Summer" }
			};

			foreach (var s in Semesters)
			{
				_db.Semesters.Add(s);
			}
			_db.SaveChanges();

			//****************************************************************************** Semesters

			// Seed the SemestersInstances
			// - SemesterInstanceName
			// - ScheduleStatus
			// - StartDate
			// - EndDate
			// - RegistrationDate
			// - EndRegistrationDate
			// - SemesterId (FK)

			var SemesterInstances = new List<SemesterInstance>
			{
				new SemesterInstance { 
					SemesterInstanceName = "Fall 2021", 
					ScheduleStatus = "Open", 
					StartDate = new DateTime(2021, 8, 23), 
					EndDate = new DateTime(2021, 12, 10), 
					RegistrationDate = new DateTime(2021, 4, 1), 
					EndRegistrationDate = new DateTime(2021, 8, 22), 
					SemesterId = 1 
				},
				new SemesterInstance
				{
					SemesterInstanceName = "Spring 2022", 
					ScheduleStatus = "Open", 
					StartDate = new DateTime(2022, 1, 10), 
					EndDate = new DateTime(2022, 4, 29), 
					RegistrationDate = new DateTime(2021, 10, 1), 
					EndRegistrationDate = new DateTime(2022, 1, 9), 
					SemesterId = 2 
				},
				new SemesterInstance
				{
					SemesterInstanceName = "Summer 2022", 
					ScheduleStatus = "Open", 
					StartDate = new DateTime(2022, 5, 9), 
					EndDate = new DateTime(2022, 8, 5), 
					RegistrationDate = new DateTime(2022, 1, 1), 
					EndRegistrationDate = new DateTime(2022, 5, 8), 
					SemesterId = 3 
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

			var Modalities = new List<Modality>
			{
				new Modality { ModalityName = "Face to Face" },
				new Modality { ModalityName = "Online" },
				new Modality { ModalityName = "Hybrid" },
				new Modality { ModalityName = "Flex" }
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
					InstructorId = 1,
					ModalityId = 1,
					ClassroomId = 1,
					TimeBlockId = 1,
					DaysOfWeekId = 1,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1
				},
				new CourseSection
				{
					BannerCRN = "23456",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 2,
					SemesterInstanceId = 1,
					InstructorId = 1,
					ModalityId = 1,
					ClassroomId = 2,
					TimeBlockId = 2,
					DaysOfWeekId = 2,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1
				},
				new CourseSection
				{
					BannerCRN = "34567",
					SectionNotes = "None",
					SectionFirstDayEnrollment = new DateTime(2021, 6, 23),
					SectionFinalEnrollment = new DateTime(2021, 8, 23),
					SectionUpdated = new DateTime(2021, 8, 23),
					SectionBannerUpdated = new DateTime(2021, 8, 23),
					CourseId = 3,
					SemesterInstanceId = 1,
					InstructorId = 1,
					ModalityId = 1,
					ClassroomId = 3,
					TimeBlockId = 3,
					DaysOfWeekId = 3,
					PartOfTermId = 1,
					PayModelId = 1,
					PayOrganizationId = 1,
					SectionStatusId = 1
				},
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

			var ReleaseTimes = new List<ReleaseTime>
			{
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 1, InstructorId = 1 },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 2, InstructorId = 1 },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 3, InstructorId = 1 },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 1, InstructorId = 2 },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 2, InstructorId = 2 },
				new ReleaseTime { ReleaseTimeAmount = 3, ReleaseTimeNotes = "None", SemesterId = 3, InstructorId = 2 }
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

			var LoadReqs = new List<LoadReq>
			{
				new LoadReq { LoadReqHours = 12, InstructorId = 1, SemesterId = 1 },
				new LoadReq { LoadReqHours = 12, InstructorId = 1, SemesterId = 2 },
				new LoadReq { LoadReqHours = 12, InstructorId = 1, SemesterId = 3 },
				new LoadReq { LoadReqHours = 12, InstructorId = 2, SemesterId = 1 },
				new LoadReq { LoadReqHours = 12, InstructorId = 2, SemesterId = 2 },
				new LoadReq { LoadReqHours = 12, InstructorId = 2, SemesterId = 3 }
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

			var Templates = new List<Template>
			{
				new Template { Quantity = 1, SemesterId = 1, CourseId = 1 },
				new Template { Quantity = 1, SemesterId = 1, CourseId = 2 },
				new Template { Quantity = 1, SemesterId = 1, CourseId = 3 },
				new Template { Quantity = 1, SemesterId = 2, CourseId = 1 },
				new Template { Quantity = 1, SemesterId = 2, CourseId = 2 },
				new Template { Quantity = 1, SemesterId = 2, CourseId = 3 },
				new Template { Quantity = 1, SemesterId = 3, CourseId = 1 },
				new Template { Quantity = 1, SemesterId = 3, CourseId = 2 },
				new Template { Quantity = 1, SemesterId = 3, CourseId = 3 }
			};

			foreach (var t in Templates)
			{
				_db.Templates.Add(t);
			}
			_db.SaveChanges();

			//****************************************************************************** Templates

			// Seed the Wishlist
			// - StudentId (FK)
			// - SemesterInstanceId (FK)

			var Wishlists = new List<Wishlist>
			{
				new Wishlist { StudentId = 1, SemesterInstanceId = 1 },
				new Wishlist { StudentId = 1, SemesterInstanceId = 2 },
				new Wishlist { StudentId = 1, SemesterInstanceId = 3 },
				new Wishlist { StudentId = 2, SemesterInstanceId = 1 },
				new Wishlist { StudentId = 2, SemesterInstanceId = 2 },
				new Wishlist { StudentId = 2, SemesterInstanceId = 3 },
				new Wishlist { StudentId = 3, SemesterInstanceId = 1 },
				new Wishlist { StudentId = 3, SemesterInstanceId = 2 },
				new Wishlist { StudentId = 3, SemesterInstanceId = 3 },
				new Wishlist { StudentId = 4, SemesterInstanceId = 1 },
				new Wishlist { StudentId = 4, SemesterInstanceId = 2 },
				new Wishlist { StudentId = 4, SemesterInstanceId = 3 }
			};

			foreach (var w in Wishlists)
			{
				_db.Wishlists.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** Wishlists

			// Seed the WishlistDetails
			// - WishlistId (FK)
			// - CourseId (FK)

			var WishlistDetails = new List<WishlistDetail>
			{ 
				new WishlistDetail { WishlistId = 1, CourseId = 1 },
				new WishlistDetail { WishlistId = 1, CourseId = 2 },
				new WishlistDetail { WishlistId = 1, CourseId = 3 },
				new WishlistDetail { WishlistId = 2, CourseId = 1 },
				new WishlistDetail { WishlistId = 2, CourseId = 2 },
				new WishlistDetail { WishlistId = 2, CourseId = 3 },
				new WishlistDetail { WishlistId = 3, CourseId = 1 },
				new WishlistDetail { WishlistId = 3, CourseId = 2 },
				new WishlistDetail { WishlistId = 3, CourseId = 3 },
				new WishlistDetail { WishlistId = 4, CourseId = 1 },
				new WishlistDetail { WishlistId = 4, CourseId = 2 },
				new WishlistDetail { WishlistId = 4, CourseId = 3 }
			};

			foreach (var w in WishlistDetails)
			{
				_db.WishlistDetails.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** WishlistDetails

			// Seed the WishlistDetailModalities
			// - WishlistPartOfDay 
			// - WishlistDetailId (FK)
			// - ModalityId (FK)
			// - DaysOfWeekId (FK)

			var WishlistDetailModalities = new List<WishlistDetailModality>
			{ 
				new WishlistDetailModality { WishlistPartOfDay = "Morning", WishlistDetailId = 1, ModalityId = 1, DaysOfWeekId = 1 },
				new WishlistDetailModality { WishlistPartOfDay = "Morning", WishlistDetailId = 1, ModalityId = 2, DaysOfWeekId = 2 },
				new WishlistDetailModality { WishlistPartOfDay = "Morning", WishlistDetailId = 1, ModalityId = 1, DaysOfWeekId = 3 },
				new WishlistDetailModality { WishlistPartOfDay = "Morning", WishlistDetailId = 1, ModalityId = 2, DaysOfWeekId = 4 },
				new WishlistDetailModality { WishlistPartOfDay = "Morning", WishlistDetailId = 1, ModalityId = 1, DaysOfWeekId = 5 },
				new WishlistDetailModality { WishlistPartOfDay = "Afternoon", WishlistDetailId = 2, ModalityId = 3, DaysOfWeekId = 1 },
				new WishlistDetailModality { WishlistPartOfDay = "Evening", WishlistDetailId = 2, ModalityId = 4, DaysOfWeekId = 2 },
				new WishlistDetailModality { WishlistPartOfDay = "Afternoon", WishlistDetailId = 3, ModalityId = 3, DaysOfWeekId = 3 },
				new WishlistDetailModality { WishlistPartOfDay = "Evening", WishlistDetailId = 3, ModalityId = 1, DaysOfWeekId = 4 },
				new WishlistDetailModality { WishlistPartOfDay = "Afternoon", WishlistDetailId = 4, ModalityId = 3, DaysOfWeekId = 5 }
			};

			foreach (var w in WishlistDetailModalities)
			{
				_db.WishlistDetailModalities.Add(w);
			}
			_db.SaveChanges();

			//****************************************************************************** WishlistDetailModalities

			// Seed the PreferenceLists
			// - InstructorId (FK)
			// - SemesterInstanceId (FK)

			var PreferenceLists = new List<PreferenceList>
			{ 
				new PreferenceList { InstructorId = 1, SemesterInstanceId = 1 },
				new PreferenceList { InstructorId = 1, SemesterInstanceId = 2 },
				new PreferenceList { InstructorId = 1, SemesterInstanceId = 3 },
				new PreferenceList { InstructorId = 2, SemesterInstanceId = 1 },
				new PreferenceList { InstructorId = 2, SemesterInstanceId = 2 },
				new PreferenceList { InstructorId = 2, SemesterInstanceId = 3 }
			};

			foreach (var p in PreferenceLists)
			{
				_db.PreferenceLists.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PreferenceLists

			// Seed the PreferenceListDetails
			// - PreferenceRank
			// - PreferenceListId (FK)
			// - CourseId (FK)

			var PreferenceListDetails = new List<PreferenceListDetail>
			{
				new PreferenceListDetail { PreferenceRank = 1, PreferenceListId = 1, CourseId = 1 },
				new PreferenceListDetail { PreferenceRank = 2, PreferenceListId = 1, CourseId = 2 },
				new PreferenceListDetail { PreferenceRank = 3, PreferenceListId = 1, CourseId = 3 },
				new PreferenceListDetail { PreferenceRank = 2, PreferenceListId = 2, CourseId = 1 },
				new PreferenceListDetail { PreferenceRank = 3, PreferenceListId = 2, CourseId = 2 },
				new PreferenceListDetail { PreferenceRank = 1, PreferenceListId = 2, CourseId = 3 },
				new PreferenceListDetail { PreferenceRank = 3, PreferenceListId = 3, CourseId = 1 },
				new PreferenceListDetail { PreferenceRank = 1, PreferenceListId = 3, CourseId = 2 },
				new PreferenceListDetail { PreferenceRank = 2, PreferenceListId = 3, CourseId = 3 },
				new PreferenceListDetail { PreferenceRank = 1, PreferenceListId = 4, CourseId = 1 },
				new PreferenceListDetail { PreferenceRank = 3, PreferenceListId = 4, CourseId = 2 },
				new PreferenceListDetail { PreferenceRank = 2, PreferenceListId = 4, CourseId = 3 }
			};

			foreach (var p in PreferenceListDetails)
			{
				_db.PreferenceListDetails.Add(p);
			}
			_db.SaveChanges();

			//****************************************************************************** PreferenceListDetails

			// Seed the PreferenceListDetailModalities
			// - PreferenceListDetailId (FK)
			// - ModalityId (FK)
			// - DaysOfWeekId (FK)
			// - TimeBlockId (FK)

			var PreferenceListDetailModalities = new List<PreferenceListDetailModality>
			{
				new PreferenceListDetailModality { PreferenceListDetailId = 1, ModalityId = 1, DaysOfWeekId = 1, TimeBlockId = 1 },
				new PreferenceListDetailModality { PreferenceListDetailId = 1, ModalityId = 2, DaysOfWeekId = 2, TimeBlockId = 2 },
				new PreferenceListDetailModality { PreferenceListDetailId = 1, ModalityId = 1, DaysOfWeekId = 3, TimeBlockId = 3 },
				new PreferenceListDetailModality { PreferenceListDetailId = 1, ModalityId = 2, DaysOfWeekId = 4, TimeBlockId = 1 },
				new PreferenceListDetailModality { PreferenceListDetailId = 1, ModalityId = 1, DaysOfWeekId = 5, TimeBlockId = 2 },
				new PreferenceListDetailModality { PreferenceListDetailId = 2, ModalityId = 3, DaysOfWeekId = 1, TimeBlockId = 3 },
				new PreferenceListDetailModality { PreferenceListDetailId = 2, ModalityId = 4, DaysOfWeekId = 2, TimeBlockId = 1 },
				new PreferenceListDetailModality { PreferenceListDetailId = 3, ModalityId = 3, DaysOfWeekId = 3, TimeBlockId = 2 },
				new PreferenceListDetailModality { PreferenceListDetailId = 3, ModalityId = 1, DaysOfWeekId = 4, TimeBlockId = 3 },
				new PreferenceListDetailModality { PreferenceListDetailId = 4, ModalityId = 3, DaysOfWeekId = 5, TimeBlockId = 1 }
			};

			foreach (var p in PreferenceListDetailModalities)
			{
				_db.PreferenceListDetailModalities.Add(p);
			}
			_db.SaveChanges();
		}
	}
}

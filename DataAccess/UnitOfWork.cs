using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<AcademicProgram> _AcademicProgram;

        public IGenericRepository<Building> _Building;

        public IGenericRepository<Campus> _Campus;

        public IGenericRepository<Classroom> _Classroom;

        public IGenericRepository<ClassroomAmenity> _ClassroomAmenity;

        public IGenericRepository<ClassroomAmenityPossession> _ClassroomAmenityPossession;

        public IGenericRepository<Course> _Course;

        public IGenericRepository<DaysOfWeek> _DaysOfWeek;

        public IGenericRepository<LoadReq> _LoadReq;

        public IGenericRepository<Modality> _Modality;

        public IGenericRepository<PartOfDay> _PartOfDay;

        public IGenericRepository<PartOfTerm> _PartOfTerm;

        public IGenericRepository<PayModel> _PayModel;

        public IGenericRepository<PayOrganization> _PayOrganization;

        public IGenericRepository<ProgramAssignment> _ProgramAssignment;

        public IGenericRepository<ReleaseTime> _ReleaseTime;

        public IGenericRepository<CourseSection> _CourseSection;

        public IGenericRepository<SectionStatus> _SectionStatus;

        public IGenericRepository<Semester> _Semester;

        public IGenericRepository<SemesterInstance> _SemesterInstance;

        public IGenericRepository<Template> _Template;

        public IGenericRepository<TimeBlock> _TimeBlock;

		public IGenericRepository<Wishlist> _Wishlist;

		public IGenericRepository<WishlistCampus> _WishlistCampus;

		public IGenericRepository<WishlistCourse> _WishlistCourse;

		public IGenericRepository<WishlistDaysOfWeek> _WishlistDaysOfWeek;

		public IGenericRepository<WishlistModality> _WishlistModality;

		public IGenericRepository<WishlistPartOfDay> _WishlistPartOfDay;

        public IGenericRepository<WishlistTimeBlock> _WishlistTimeBlock;

        //Added After Identity Framework Scaffolding
        public IGenericRepository<ApplicationUser> _ApplicationUser;

        //These ones will eventually not be needed
        //public IGenericRepository<Role> _Role;
        //public IGenericRepository<User> _User;
        //public IGenericRepository<RoleAssignment> _RoleAssignment;

        public IGenericRepository<AcademicProgram> AcademicProgram
        {
            get
            {
                if (_AcademicProgram == null)
                {
                    _AcademicProgram = new GenericRepository<AcademicProgram>(_dbContext);
                }

                return _AcademicProgram;
            }
        }

        public IGenericRepository<Building> Building
        {
            get
            {
                if (_Building == null)
                {
                    _Building = new GenericRepository<Building>(_dbContext);
                }

                return _Building;
            }
        }

        public IGenericRepository<Campus> Campus
        {
            get
            {
                if (_Campus == null)
                {
                    _Campus = new GenericRepository<Campus>(_dbContext);
                }

                return _Campus;
            }
        }

        public IGenericRepository<Classroom> Classroom
        {
            get
            {
                if (_Classroom == null)
                {
                    _Classroom = new GenericRepository<Classroom>(_dbContext);
                }

                return _Classroom;
            }
        }

        public IGenericRepository<ClassroomAmenity> ClassroomAmenity
        {
            get
            {
                if (_ClassroomAmenity == null)
                {
                    _ClassroomAmenity = new GenericRepository<ClassroomAmenity>(_dbContext);
                }

                return _ClassroomAmenity;
            }
        }

        public IGenericRepository<ClassroomAmenityPossession> ClassroomAmenityPossession
        {
            get
            {
                if (_ClassroomAmenityPossession == null)
                {
                    _ClassroomAmenityPossession = new GenericRepository<ClassroomAmenityPossession>(_dbContext);
                }

                return _ClassroomAmenityPossession;
            }
        }

        public IGenericRepository<Course> Course
        {
            get
            {
                if (_Course == null)
                {
                    _Course = new GenericRepository<Course>(_dbContext);
                }

                return _Course;
            }
        }

        public IGenericRepository<CourseSection> CourseSection
        {
            get
            {
                if (_CourseSection == null)
                {
                    _CourseSection = new GenericRepository<CourseSection>(_dbContext);
                }

                return _CourseSection;
            }
        }

        public IGenericRepository<DaysOfWeek> DaysOfWeek
        {
            get
            {
                if (_DaysOfWeek == null)
                {
                    _DaysOfWeek = new GenericRepository<DaysOfWeek>(_dbContext);
                }

                return _DaysOfWeek;
            }
        }

        public IGenericRepository<LoadReq> LoadReq
        {
            get
            {
                if (_LoadReq == null)
                {
                    _LoadReq = new GenericRepository<LoadReq>(_dbContext);
                }

                return _LoadReq;
            }
        }

        public IGenericRepository<Modality> Modality
        {
            get
            {
                if (_Modality == null)
                {
                    _Modality = new GenericRepository<Modality>(_dbContext);
                }

                return _Modality;
            }
        }

		public IGenericRepository<PartOfDay> PartOfDay
		{
			get
			{
				if (_PartOfDay == null)
				{
					_PartOfDay = new GenericRepository<PartOfDay>(_dbContext);
				}

				return _PartOfDay;
			}
		}

		public IGenericRepository<PartOfTerm> PartOfTerm
        {
            get
            {
                if (_PartOfTerm == null)
                {
                    _PartOfTerm = new GenericRepository<PartOfTerm>(_dbContext);
                }

                return _PartOfTerm;
            }
        }

        public IGenericRepository<PayModel> PayModel
        {
            get
            {
                if (_PayModel == null)
                {
                    _PayModel = new GenericRepository<PayModel>(_dbContext);
                }

                return _PayModel;
            }
        }

        public IGenericRepository<PayOrganization> PayOrganization
        {
            get
            {
                if (_PayOrganization == null)
                {
                    _PayOrganization = new GenericRepository<PayOrganization>(_dbContext);
                }

                return _PayOrganization;
            }
        }

        public IGenericRepository<ProgramAssignment> ProgramAssignment
        {
            get
            {
                if (_ProgramAssignment == null)
                {
                    _ProgramAssignment = new GenericRepository<ProgramAssignment>(_dbContext);
                }

                return _ProgramAssignment;
            }
        }

        public IGenericRepository<ReleaseTime> ReleaseTime
        {
            get
            {
                if (_ReleaseTime == null)
                {
                    _ReleaseTime = new GenericRepository<ReleaseTime>(_dbContext);
                }

                return _ReleaseTime;
            }
        }

        public IGenericRepository<SectionStatus> SectionStatus
        {
            get
            {
                if (_SectionStatus == null)
                {
                    _SectionStatus = new GenericRepository<SectionStatus>(_dbContext);
                }

                return _SectionStatus;
            }
        }

        public IGenericRepository<Semester> Semester
        {
            get
            {
                if (_Semester == null)
                {
                    _Semester = new GenericRepository<Semester>(_dbContext);
                }

                return _Semester;
            }
        }

        public IGenericRepository<SemesterInstance> SemesterInstance
        {
            get
            {
                if (_SemesterInstance == null)
                {
                    _SemesterInstance = new GenericRepository<SemesterInstance>(_dbContext);
                }

                return _SemesterInstance;
            }
        }

        public IGenericRepository<Template> Template
        {
            get
            {
                if (_Template == null)
                {
                    _Template = new GenericRepository<Template>(_dbContext);
                }

                return _Template;
            }
        }

        public IGenericRepository<TimeBlock> TimeBlock
        {
            get
            {
                if (_TimeBlock == null)
                {
                    _TimeBlock = new GenericRepository<TimeBlock>(_dbContext);
                }

                return _TimeBlock;
            }
        }

        public IGenericRepository<Wishlist> Wishlist
        {
            get
            {
                if (_Wishlist == null)
                {
                    _Wishlist = new GenericRepository<Wishlist>(_dbContext);
                }

                return _Wishlist;
            }
        }

		public IGenericRepository<WishlistCampus> WishlistCampus
		{
			get
			{
				if (_WishlistCampus == null)
				{
					_WishlistCampus = new GenericRepository<WishlistCampus>(_dbContext);
				}

				return _WishlistCampus;
			}
		}

		public IGenericRepository<WishlistCourse> WishlistCourse
		{
			get
			{
				if (_WishlistCourse == null)
				{
					_WishlistCourse = new GenericRepository<WishlistCourse>(_dbContext);
				}

				return _WishlistCourse;
			}
		}

		public IGenericRepository<WishlistDaysOfWeek> WishlistDaysOfWeek
		{
			get
			{
				if (_WishlistDaysOfWeek == null)
				{
					_WishlistDaysOfWeek = new GenericRepository<WishlistDaysOfWeek>(_dbContext);
				}

				return _WishlistDaysOfWeek;
			}
		}

		public IGenericRepository<WishlistModality> WishlistModality
		{
			get
			{
				if (_WishlistModality == null)
				{
					_WishlistModality = new GenericRepository<WishlistModality>(_dbContext);
				}

				return _WishlistModality;
			}
		}

		public IGenericRepository<WishlistPartOfDay> WishlistPartOfDay
		{
			get
			{
				if (_WishlistPartOfDay == null)
				{
					_WishlistPartOfDay = new GenericRepository<WishlistPartOfDay>(_dbContext);
				}

				return _WishlistPartOfDay;
			}
		}

		public IGenericRepository<WishlistTimeBlock> WishlistTimeBlock
		{
			get
			{
				if (_WishlistTimeBlock == null)
				{
					_WishlistTimeBlock = new GenericRepository<WishlistTimeBlock>(_dbContext);
				}

				return _WishlistTimeBlock;
			}
		}

		public IGenericRepository<ApplicationUser> ApplicationUser
        {
            get
            {
                if (_ApplicationUser == null)
                {
                    _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
                }

                return _ApplicationUser;
            }
        }

        //public IGenericRepository<User> User
        //{
        //    get
        //    {
        //        if (_User == null)
        //        {
        //            _User = new GenericRepository<User>(_dbContext);
        //        }

        //        return _User;
        //    }
        //}

        //public IGenericRepository<RoleAssignment> RoleAssignment
        //{
        //    get
        //    {
        //        if (_RoleAssignment == null)
        //        {
        //            _RoleAssignment = new GenericRepository<RoleAssignment>(_dbContext);
        //        }

        //        return _RoleAssignment;
        //    }
        //}

        //public IGenericRepository<Role> Role
        //{
        //    get
        //    {
        //        if (_Role == null)
        //        {
        //            _Role = new GenericRepository<Role>(_dbContext);
        //        }

        //        return _Role;
        //    }
        //}

		IGenericRepository<AcademicProgram> IUnitOfWork.AcademicProgram => throw new NotImplementedException();

		IGenericRepository<Building> IUnitOfWork.Building => throw new NotImplementedException();

		IGenericRepository<Campus> IUnitOfWork.Campus => throw new NotImplementedException();

		IGenericRepository<Classroom> IUnitOfWork.Classroom => throw new NotImplementedException();

		IGenericRepository<ClassroomAmenity> IUnitOfWork.ClassroomAmenity => throw new NotImplementedException();

		IGenericRepository<ClassroomAmenityPossession> IUnitOfWork.ClassroomAmenityPossession => throw new NotImplementedException();

		IGenericRepository<Course> IUnitOfWork.Course => throw new NotImplementedException();

		IGenericRepository<DaysOfWeek> IUnitOfWork.DaysOfWeek => throw new NotImplementedException();

		IGenericRepository<LoadReq> IUnitOfWork.LoadReq => throw new NotImplementedException();

		IGenericRepository<Modality> IUnitOfWork.Modality => throw new NotImplementedException();

		IGenericRepository<PartOfDay> IUnitOfWork.PartOfDay => throw new NotImplementedException();

		IGenericRepository<PartOfTerm> IUnitOfWork.PartOfTerm => throw new NotImplementedException();

		IGenericRepository<PayModel> IUnitOfWork.PayModel => throw new NotImplementedException();

		IGenericRepository<PayOrganization> IUnitOfWork.PayOrganization => throw new NotImplementedException();

		IGenericRepository<ProgramAssignment> IUnitOfWork.ProgramAssignment => throw new NotImplementedException();

		IGenericRepository<ReleaseTime> IUnitOfWork.ReleaseTime => throw new NotImplementedException();

		IGenericRepository<CourseSection> IUnitOfWork.CourseSection => throw new NotImplementedException();

		IGenericRepository<SectionStatus> IUnitOfWork.SectionStatus => throw new NotImplementedException();

		IGenericRepository<Semester> IUnitOfWork.Semester => throw new NotImplementedException();

		IGenericRepository<SemesterInstance> IUnitOfWork.SemesterInstance => throw new NotImplementedException();

		IGenericRepository<Template> IUnitOfWork.Template => throw new NotImplementedException();

		IGenericRepository<TimeBlock> IUnitOfWork.TimeBlock => throw new NotImplementedException();

		IGenericRepository<Wishlist> IUnitOfWork.Wishlist => throw new NotImplementedException();

		IGenericRepository<WishlistCampus> IUnitOfWork.WishlistCampus => throw new NotImplementedException();

		IGenericRepository<WishlistCourse> IUnitOfWork.WishlistCourse => throw new NotImplementedException();

		IGenericRepository<WishlistDaysOfWeek> IUnitOfWork.WishlistDaysOfWeek => throw new NotImplementedException();

		IGenericRepository<WishlistModality> IUnitOfWork.WishlistModality => throw new NotImplementedException();

		IGenericRepository<WishlistPartOfDay> IUnitOfWork.WishlistPartOfDay => throw new NotImplementedException();

		IGenericRepository<WishlistTimeBlock> IUnitOfWork.WishlistTimeBlock => throw new NotImplementedException();

		//IGenericRepository<Role> IUnitOfWork.Role => throw new NotImplementedException();

		//IGenericRepository<User> IUnitOfWork.User => throw new NotImplementedException();

		//IGenericRepository<RoleAssignment> IUnitOfWork.RoleAssignment => throw new NotImplementedException();

		public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

		int IUnitOfWork.Commit()
		{
			throw new NotImplementedException();
		}

		Task<int> IUnitOfWork.CommitAsync()
		{
			throw new NotImplementedException();
		}
	}
}

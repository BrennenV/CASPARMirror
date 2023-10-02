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

        public IGenericRepository<Instructor> _Instructor;

        public IGenericRepository<LoadReq> _LoadReq;

        public IGenericRepository<Modality> _Modality;

        public IGenericRepository<PartOfTerm> _PartOfTerm;

        public IGenericRepository<PayModel> _PayModel;

        public IGenericRepository<PayOrganization> _PayOrganization;

        public IGenericRepository<PreferenceList> _PreferenceList;

        public IGenericRepository<PreferenceListDetail> _PreferenceListDetail;

        public IGenericRepository<PreferenceListDetailModality> _PreferenceListDetailModality;

        public IGenericRepository<ProgramAssignment> _ProgramAssignment;

        public IGenericRepository<ReleaseTime> _ReleaseTime;

        public IGenericRepository<CourseSection> _CourseSection;

        public IGenericRepository<SectionStatus> _SectionStatus;

        public IGenericRepository<Semester> _Semester;

        public IGenericRepository<SemesterInstance> _SemesterInstance;

        public IGenericRepository<Student> _Student;

        public IGenericRepository<Template> _Template;

        public IGenericRepository<TimeBlock> _TimeBlock;

        public IGenericRepository<Wishlist> _Wishlist;

        public IGenericRepository<WishlistDetail> _WishlistDetail;

        public IGenericRepository<WishlistDetailModality> _WishlistDetailModality;

        //These ones will eventually not be needed
        public IGenericRepository<Role> _Role;
        public IGenericRepository<User> _User;
        public IGenericRepository<RoleAssignment> _RoleAssignment;

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

        public IGenericRepository<Instructor> Instructor
        {
            get
            {
                if (_Instructor == null)
                {
                    _Instructor = new GenericRepository<Instructor>(_dbContext);
                }

                return _Instructor;
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

        public IGenericRepository<PreferenceList> PreferenceList
        {
            get
            {
                if (_PreferenceList == null)
                {
                    _PreferenceList = new GenericRepository<PreferenceList>(_dbContext);
                }

                return _PreferenceList;
            }
        }

        public IGenericRepository<PreferenceListDetail> PreferenceListDetail
        {
            get
            {
                if (_PreferenceListDetail == null)
                {
                    _PreferenceListDetail = new GenericRepository<PreferenceListDetail>(_dbContext);
                }

                return _PreferenceListDetail;
            }
        }

        public IGenericRepository<PreferenceListDetailModality> PreferenceListDetailModality
        {
            get
            {
                if (_PreferenceListDetailModality == null)
                {
                    _PreferenceListDetailModality = new GenericRepository<PreferenceListDetailModality>(_dbContext);
                }

                return _PreferenceListDetailModality;
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

        public IGenericRepository<Student> Student
        {
            get
            {
                if (_Student == null)
                {
                    _Student = new GenericRepository<Student>(_dbContext);
                }

                return _Student;
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

        public IGenericRepository<WishlistDetail> WishlistDetail
        {
            get
            {
                if (_WishlistDetail == null)
                {
                    _WishlistDetail = new GenericRepository<WishlistDetail>(_dbContext);
                }

                return _WishlistDetail;
            }
        }

        public IGenericRepository<WishlistDetailModality> WishlistDetailModality
        {
            get
            {
                if (_WishlistDetailModality == null)
                {
                    _WishlistDetailModality = new GenericRepository<WishlistDetailModality>(_dbContext);
                }

                return _WishlistDetailModality;
            }
        }

        public IGenericRepository<User> User
        {
            get
            {
                if (_User == null)
                {
                    _User = new GenericRepository<User>(_dbContext);
                }

                return _User;
            }
        }

        public IGenericRepository<RoleAssignment> RoleAssignment
        {
            get
            {
                if (_RoleAssignment == null)
                {
                    _RoleAssignment = new GenericRepository<RoleAssignment>(_dbContext);
                }

                return _RoleAssignment;
            }
        }

        public IGenericRepository<Role> Role
        {
            get
            {
                if (_Role == null)
                {
                    _Role = new GenericRepository<Role>(_dbContext);
                }

                return _Role;
            }
        }

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
    }
}

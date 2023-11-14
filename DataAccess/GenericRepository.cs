using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // By using ReadOnly ApplicationDbContext, you can have access to only
        // querying capabilities of DbContext. UnitOfWork actually writes
        // (commits) to the PHYSICAL tables (not internal object).
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            if (typeof(T) == typeof(Campus)) {
                Campus temp;
                temp = entity as Campus;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Campus>().Update(temp);
            } else if (typeof(T) == typeof(Building)) {
                Building temp;
                temp = entity as Building;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Building>().Update(temp);
            } else if (typeof(T) == typeof(ClassroomAmenity)) {
                ClassroomAmenity temp;
                temp = entity as ClassroomAmenity;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<ClassroomAmenity>().Update(temp);
            } else if (typeof(T) == typeof(ClassroomAmenityPossession)) {
                ClassroomAmenityPossession temp;
                temp = entity as ClassroomAmenityPossession;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<ClassroomAmenityPossession>().Update(temp);
            } else if (typeof(T) == typeof(Classroom)) {
                Classroom temp;
                temp = entity as Classroom;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Classroom>().Update(temp);
            } else if (typeof(T) == typeof(Course)) {
                Course temp;
                temp = entity as Course;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Course>().Update(temp);
            } else if (typeof(T) == typeof(CourseSection)) {
                CourseSection temp;
                temp = entity as CourseSection;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<CourseSection>().Update(temp);
            } else if (typeof(T) == typeof(DaysOfWeek)) {
                DaysOfWeek temp;
                temp = entity as DaysOfWeek;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<DaysOfWeek>().Update(temp);
            } else if (typeof(T) == typeof(LoadReq)) {
                LoadReq temp;
                temp = entity as LoadReq;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<LoadReq>().Update(temp);
            } else if (typeof(T) == typeof(Modality)) {
                Modality temp;
                temp = entity as Modality;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Modality>().Update(temp);
            } else if (typeof(T) == typeof(PartOfDay)) {
                PartOfDay temp;
                temp = entity as PartOfDay;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<PartOfDay>().Update(temp);
            } else if (typeof(T) == typeof(PartOfTerm)) {
                PartOfTerm temp;
                temp = entity as PartOfTerm;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<PartOfTerm>().Update(temp);
            } else if (typeof(T) == typeof(PayModel)) {
                PayModel temp;
                temp = entity as PayModel;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<PayModel>().Update(temp);
            } else if (typeof(T) == typeof(PayOrganization)) {
                PayOrganization temp;
                temp = entity as PayOrganization;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<PayOrganization>().Update(temp);
            } else if (typeof(T) == typeof(ProgramAssignment)) {
                ProgramAssignment temp;
                temp = entity as ProgramAssignment;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<ProgramAssignment>().Update(temp);
            } else if (typeof(T) == typeof(ReleaseTime)) {
                ReleaseTime temp;
                temp = entity as ReleaseTime;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<ReleaseTime>().Update(temp);
            } else if (typeof(T) == typeof(SectionStatus)) {
                SectionStatus temp;
                temp = entity as SectionStatus;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<SectionStatus>().Update(temp);
            } else if (typeof(T) == typeof(SemesterInstance)) {
                SemesterInstance temp;
                temp = entity as SemesterInstance;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<SemesterInstance>().Update(temp);
            } else if (typeof(T) == typeof(Semester)) {
                Semester temp;
                temp = entity as Semester;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Semester>().Update(temp);
            } else if (typeof(T) == typeof(Template)) {
                Template temp;
                temp = entity as Template;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Template>().Update(temp);
            } else if (typeof(T) == typeof(TimeBlock)) {
                TimeBlock temp;
                temp = entity as TimeBlock;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<TimeBlock>().Update(temp);
            }else if (typeof(T) == typeof(WishlistCampus)) {
                WishlistCampus temp;
                temp = entity as WishlistCampus;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistCampus>().Update(temp);
            } else if (typeof(T) == typeof(WishlistCourse)) {
                WishlistCourse temp;
                temp = entity as WishlistCourse;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistCourse>().Update(temp);
            } else if (typeof(T) == typeof(WishlistDaysOfWeek)) {
                WishlistDaysOfWeek temp;
                temp = entity as WishlistDaysOfWeek;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistDaysOfWeek>().Update(temp);
            } else if (typeof(T) == typeof(WishlistModality)) {
                WishlistModality temp;
                temp = entity as WishlistModality;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistModality>().Update(temp);
            } else if (typeof(T) == typeof(WishlistPartOfDay)) {
                WishlistPartOfDay temp;
                temp = entity as WishlistPartOfDay;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistPartOfDay>().Update(temp);
            } else if (typeof(T) == typeof(Wishlist)) {
                Wishlist temp;
                temp = entity as Wishlist;
                if (temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<Wishlist>().Update(temp);
            } else if (typeof(T) == typeof(WishlistTimeBlock)) {
                WishlistTimeBlock temp;
                temp = entity as WishlistTimeBlock;
                if(temp == null) {
                    throw new("Null value. Unable to Archive a value of type: '" + typeof(T) + "' Type casting returned null.");
                }
                if (temp.IsArchived != true) {
                    temp.IsArchived = true;
                } else {
                    temp.IsArchived = false;
                }
                _dbContext.Set<WishlistTimeBlock>().Update(temp);
            } else {
                throw new("Unable to Archive a value of type: '" + typeof(T) + "'");
            }


            //_dbContext.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        // The virtual keyword is used to modify a method, property, indexer or
        // and allows for it to be overridden in a derived class.
        public virtual T Get(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            if (includes == null) //we are not joining other objects
            {
                if (!trackChanges) //is false
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
            else //we have includes to deal with 
            {
                //includes = "Comma,Separated,Objects,Without,Spaces"
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (var includePropery in includes.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includePropery);
                }

                if (!trackChanges) //is false
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return _dbContext.Set<T>()
                     .Where(predicate)
                     .FirstOrDefault();
                }
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }
            //has includes
            else if (includes != null)
            {
                foreach (var includePropery in includes.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includePropery);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return queryable.OrderBy(orderBy).ToList();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return queryable
                        .Where(predicate)
                        .ToList();
                }
                else
                {
                    return queryable
                        .Where(predicate)
                        .OrderBy(orderBy)
                        .ToList();
                }
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>>? orderBy = null, string? includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }

            //has includes
            else if (includes != null)
            {
                foreach (var includePropery in includes.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includePropery);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return await queryable.OrderBy(orderBy).ToListAsync();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return await queryable
                        .Where(predicate)
                        .ToListAsync();
                }
                else
                {
                    return await queryable
                        .Where(predicate)
                        .OrderBy(orderBy)
                        .ToListAsync();
                }
            }
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, string? includes = null)
        {
            if (includes == null) //we are not joining other objects
            {
                if (!trackChanges) //is false
                {   
                    return await _dbContext.Set<T>()
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return await _dbContext.Set<T>()
                     .Where(predicate)
                     .FirstOrDefaultAsync();
                }
            }
            else //we have includes to deal with 
            {
                //includes = "Comma,Separated,Objects,Without,Spaces"
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (var includePropery in includes.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includePropery);
                }

                if (!trackChanges) //is false
                {
                    return queryable
                        .Where(predicate)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else //we are tracking changes (which EF does by default)
                {
                    return queryable
                     .Where(predicate)
                     .FirstOrDefault();
                }
            }
        }

        public virtual T GetById(int? id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            //for track changes I'm flagging modified to the system
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

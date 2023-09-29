using Infrastructure.Interfaces;
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
            _dbContext.Set<T>().Remove(entity);
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

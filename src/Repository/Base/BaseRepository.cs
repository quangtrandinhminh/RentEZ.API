using System.Linq.Expressions;
using BusinessObject.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Repository.Infrastructure;
namespace Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }

                _dbSet = _context.Set<T>();
                return _dbSet;
            }
        }


        public IQueryable<T?> GetAll()
        {
            return DbSet.AsQueryable().AsNoTracking();
        }

        public IQueryable<T> GetAllWithCondition(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<T, object>>[] array = includeProperties;
                foreach (Expression<Func<T, object>> navigationPropertyPath in array)
                {
                    queryable = queryable.Include(navigationPropertyPath);
                }
            }

            return predicate == null ? queryable : queryable.Where(predicate);
        }

        public async Task<IList<T>?> GetAllAsync()
        {
            return await DbSet.AsQueryable().AsNoTracking().ToListAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null
            , bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            
            IQueryable<T> source = DbSet.AsNoTracking();
            if (predicate != null)
            {
                source = source.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<T, object>>[] array = includeProperties;
                foreach (Expression<Func<T, object>> navigationPropertyPath in array)
                {
                    source = source.Include(navigationPropertyPath);
                }
            }

            return isIncludeDeleted ? source.IgnoreQueryFilters() : source.Where((x) => x.DeletedTime == null);
        }

        public T? GetById(int id)
        {
             
            
            return DbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
             
            
            return await DbSet.FindAsync(id);
        }

        /*public  async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, 
            bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Get(predicate, isIncludeDeleted, includeProperties)
                .OrderByDescending(p => p.CreatedTime).FirstOrDefaultAsync();
        }*/

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
            bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            
            var query = DbSet.AsQueryable();

            if (!isIncludeDeleted)
            {
                query = query.Where(e => e.DeletedTime == null);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public T Add(T entity)
        {
            
            var addedEntity = DbSet.Add(entity);
            _context.Entry(entity).State = EntityState.Detached;
            return addedEntity.Entity;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
             
            _context.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T?> entities)
        {
             
            
            await DbSet.AddRangeAsync(entities);
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void Update(T entity)
        {
             
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task UpdateAsync(T entity)
        {
             
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task UpdateRangeAsync(IEnumerable<T?> entities)
        {
             
            
            DbSet.UpdateRange(entities);
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void Delete(T? entity)
        {
             
            
            DbSet.Remove(entity);
        }

        public async Task DeleteAsync(T? entity)
        {
             
            
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T?> entities)
        {
             
            
            DbSet.RemoveRange(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<T?> entities)
        {
             
            
            DbSet.RemoveRange(entities);
        }

        public IQueryable<T?> FindByCondition(Expression<Func<T?, bool>> expression)
        {
             
            
            return DbSet.Where(expression).AsQueryable().AsNoTracking();
        }

        public async Task<IList<T?>> FindByConditionAsync(Expression<Func<T?, bool>> expression)
        {
            
            return await DbSet.Where(expression).AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties)
            => await Get(predicate, includeProperties).FirstOrDefaultAsync();

        public IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
             
            
            IQueryable<T> reault = DbSet.AsNoTracking();
            if (predicate != null)
            {
                reault = reault.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<T, object>>[] array = includeProperties;
                foreach (Expression<Func<T, object>> navigationPropertyPath in array)
                {
                    reault = reault.Include(navigationPropertyPath);
                }
            }

            return reault.Where(x => x.DeletedTime == null);
        }

        public void TryAttach(T entity)
        {
            try
            {
                 
                
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            catch
            {
            }
        }

        protected void TryAttachRange(ICollection<T> entities)
        {
            try
            {
                 
                
                foreach (var entity in entities)
                {
                    if (_context.Entry(entity).State != EntityState.Detached)
                    {
                        entities.Remove(entity);
                    }
                }
                DbSet.AttachRange(entities);
            }
            catch
            {
            }
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
             
            
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        // save changes
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using System.Linq.Expressions;
using Repository.Extensions;

namespace Repository.Base;

public interface IBaseRepository<T> where T : class, new()
{
    public IQueryable<T> Set();
    void RefreshEntity(T entity);
    IQueryable<T?> GetAll();
    Task<IList<T>?> GetAllAsync();
    Task<PaginatedList<TResult>> GetAllPaginatedQueryable<TResult>(int page, int pageSize, Expression<Func<T, bool>> fillterPredicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> GetAllWithCondition(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties);
    T Add(T? entity);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<T?> entities);
    Task AddRangeAsync(IEnumerable<T?> entities);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(ICollection<T> entities);
    IQueryable<T?> FindByCondition(Expression<Func<T?, bool>> expression);
    Task<IList<T?>> FindByConditionAsync(Expression<Func<T?, bool>> expression);
    Task SaveChangesAsync();
    void SaveChanges();
}

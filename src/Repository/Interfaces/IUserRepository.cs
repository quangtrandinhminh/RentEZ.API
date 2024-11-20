using Microsoft.AspNetCore.Identity;
using Repository.Models.Identity;
using System.Linq.Expressions;
using Repository.Extensions;

namespace Repository.Interfaces
{
    public interface IUserRepository : IUserStore<UserEntity>
    {
        Task<int> SaveChangeAsync();
        Task<IdentityResult> CreateAsync(UserEntity userEntity, CancellationToken cancellationToken = default);
        Task<IdentityResult> UpdateAsync(UserEntity userEntity);
        Task<UserEntity?> GetSingleAsync(Expression<Func<UserEntity, bool>>? predicate = null, params Expression<Func<UserEntity, object>>[] includeProperties);
        IQueryable<UserEntity> GetAllWithCondition(Expression<Func<UserEntity, bool>> predicate = null, params Expression<Func<UserEntity, object>>[] includeProperties);
        Task<UserEntity?> GetUserByIdAsync(int userId);

        Task<PaginatedList<UserEntity>> GetAllPaginatedQueryable(int pageNumber,
            int pageSize,
            Expression<Func<UserEntity, bool>> predicate = null,
            Expression<Func<UserEntity, object>>? orderBy = null,
            params Expression<Func<UserEntity, object>>[]? includeProperties);
    }
}
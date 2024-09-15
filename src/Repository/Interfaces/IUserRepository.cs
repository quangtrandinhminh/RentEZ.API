using BusinessObject.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IUserRepository : IUserStore<UserEntity>
    {
        Task<int> SaveChangeAsync();
        Task<IdentityResult> CreateAsync(UserEntity userEntity, CancellationToken cancellationToken = default);
        Task<IdentityResult> UpdateAsync(UserEntity userEntity);
        Task<UserEntity?> GetSingleAsync(Expression<Func<UserEntity, bool>>? predicate = null, params Expression<Func<UserEntity, object>>[] includeProperties);
        IQueryable<UserEntity> GetAllWithCondition(Expression<Func<UserEntity, bool>> predicate = null, params Expression<Func<UserEntity, object>>[] includeProperties);
        Task<UserEntity> GetUserByIdAsync(int userId);
    }
}
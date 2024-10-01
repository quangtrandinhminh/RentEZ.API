using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models.Identity;

namespace Repository.Repositories
{
    public class UserRepository : UserStore<UserEntity, RoleEntity, AppDbContext, int>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> SaveChangeAsync() => await Context.SaveChangesAsync();

        public IQueryable<UserEntity> GetAllWithCondition(Expression<Func<UserEntity, bool>> predicate = null,
            params Expression<Func<UserEntity, object>>[] includeProperties)
        {
            var dbSet = _context.Set<UserEntity>();
            IQueryable<UserEntity> queryable = dbSet.AsNoTracking();
            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<UserEntity, object>>[] array = includeProperties;
                foreach (Expression<Func<UserEntity, object>> navigationPropertyPath in array)
                {
                    queryable = queryable.Include(navigationPropertyPath);
                }
            }

            return predicate == null ? queryable : queryable.Where(predicate);
        }

        public override async Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<UserEntity?> GetSingleAsync(Expression<Func<UserEntity, bool>>? predicate = null,
            params Expression<Func<UserEntity, object>>[] includeProperties)
        => await Get(predicate, includeProperties).FirstOrDefaultAsync();

        public IQueryable<UserEntity> Get(Expression<Func<UserEntity, bool>>? predicate = null, params Expression<Func<UserEntity, object>>[] includeProperties)
        {
            IQueryable<UserEntity> reault = _context.Users.AsNoTracking();
            if (predicate != null)
            {
                reault = reault.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<UserEntity, object>>[] array = includeProperties;
                foreach (Expression<Func<UserEntity, object>> navigationPropertyPath in array)
                {
                    reault = reault.Include(navigationPropertyPath);
                }
            }

            return reault.Where(x => x.DeletedTime == null);
        }

        public async Task<UserEntity?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
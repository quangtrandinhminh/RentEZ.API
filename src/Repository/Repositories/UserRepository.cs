using System.Linq.Expressions;
using BusinessObject.Entities;
using BusinessObject.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository : UserStore<UserEntity, RoleEntity, AppDbContext, int>, IUserRepository
    {
        private readonly AppDbContext _context = new();
        private UserManager<UserEntity> _userManager;
        private SignInManager<UserEntity> _signinManager;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> SaveChangeAsync() => await Context.SaveChangesAsync();

        public IQueryable<UserEntity> GetAllWithCondition(Expression<Func<UserEntity, bool>> predicate = null,
            params Expression<Func<UserEntity, object>>[] includeProperties)
        {
            var context = new AppDbContext();
            var dbSet = context.Set<UserEntity>();
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
            await Context.Users.AddAsync(user, cancellationToken);
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

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<List<UserEntity>> GetPendingShopkeeperListAsync()
        {
            return await  _context.Users
                .Include(x => x.ManagedShop)
                .Where(x => !x.ManagedShop.IsVerified && x.ManagedShop != null)
                .Select(u => new UserEntity
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    FullName = u.FullName,
                    Address = u.Address,
                    Avatar = u.Avatar,
                    BirthDate = u.BirthDate,
                    ManagedShop = new Shop
                    {
                        ShopEmail = u.ManagedShop.ShopEmail,
                        ShopName = u.ManagedShop.ShopName,
                        ShopPhone = u.ManagedShop.ShopPhone,
                        ShopAddress = u.ManagedShop.ShopAddress,
                        ShopAvatar = u.ManagedShop.ShopAvatar,
                    }
                })
                .ToListAsync();
        }
    }
}
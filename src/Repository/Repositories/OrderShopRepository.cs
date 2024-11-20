using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Extensions;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories;

public class OrderShopRepository : BaseRepository<OrderShop>, IOrderShopRepository
{
    public OrderShopRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<OrderShop> GetOrderShopByIdAsync(
        Expression<Func<OrderShop, bool>>? predicate = null,
        bool isIncludeDeleted = false,
        bool isIncludeProducts = false,
        params Expression<Func<OrderShop, object>>[] includeProperties)
    {
        var query = Set().AsNoTracking();

        if (!isIncludeDeleted)
        {
            query = query.Where(e => e.DeletedTime == null);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                if (isIncludeProducts)
                {
                    query = query.Include(e => e.OrderDetails).ThenInclude(os => os.Product);
                }

                query = query.Include(includeProperty);
            }
            query = query.AsSplitQuery();
        }
        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<PaginatedList<OrderShop>> GetAllOrderShopsAsync(int pageIndex, int pageSize,
        Expression<Func<OrderShop, bool>>? predicate = null,
        bool isIncludeDeleted = false,
        bool isIncludeProducts = false,
        Expression<Func<OrderShop, object>>? orderBy = null,
        params Expression<Func<OrderShop, object>>[] includeProperties)
    {
        var query = Set().AsNoTracking();

        if (!isIncludeDeleted)
        {
            query = query.Where(e => e.DeletedTime == null);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                if (isIncludeProducts)
                {
                    query = query.Include(e => e.OrderDetails).ThenInclude(os => os.Product);
                }

                query = query.Include(includeProperty);
            }
            query = query.AsSplitQuery();
        }

        // Apply the predicate
        query = predicate != null ? query.Where(predicate) : query;
        query = orderBy != null ? query.OrderBy(orderBy) : query.OrderByDescending(p => p.CreatedTime);

        return await PaginatedList<OrderShop>.CreateAsync(query, pageIndex, pageSize);
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Order> GetOrderByIdAsync(Expression<Func<Order, bool>>? predicate = null, 
        bool isIncludeDeleted = false,
        bool isIncludeTracking = false,
        bool isIncludeOrderDetails = false,
        bool isIncludeShop = false,
        bool isIncludeVoucher = false,
            params Expression<Func<Order, object>>[] includeProperties)
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
                var isIncludeOrderShops = includeProperty.ToString().Contains("OrderShops");
                if (isIncludeOrderShops)
                {
                    if (isIncludeOrderDetails)
                    {
                        query = query.Include(e => e.OrderShops).ThenInclude(os => os.OrderDetails)
                            .ThenInclude(od => od.Product);
                    }

                    if (isIncludeVoucher)
                    {
                        query = query.Include(e => e.OrderShops).ThenInclude(os => os.Voucher);
                    }

                    if (isIncludeShop)
                    {
                        query = query.Include(e => e.OrderShops).ThenInclude(os => os.Shop);
                    }

                    if (isIncludeTracking)
                    {
                        query = query.Include(e => e.OrderShops).ThenInclude(os => os.OrderTrackings);
                    }
                }

                query = query.Include(includeProperty);
            }
            query = query.AsSplitQuery();
        }
        return await query.FirstOrDefaultAsync(predicate);
    }
}
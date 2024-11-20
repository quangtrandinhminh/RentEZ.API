using System.Linq.Expressions;
using Repository.Base;
using Repository.Models;

namespace Repository.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order> GetOrderByIdAsync(Expression<Func<Order, bool>>? predicate = null,
        bool isIncludeDeleted = false,
        bool isIncludeTracking = false,
        bool isIncludeOrderDetails = false,
        bool isIncludeShop = false,
        bool isIncludeVoucher = false,
        params Expression<Func<Order, object>>[] includeProperties);
}
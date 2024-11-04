using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;
using Serilog;
using Service.ApiModels.Order;
using Service.ApiModels.OrderDetail;
using Service.BusinessModels;
using Service.Interfaces;
using Service.Mapper;
using System.Linq;
using Dasync.Collections;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using System.Security.Claims;
using EnumsNET;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Extensions;
using Service.ApiModels.OrderShop;
using System.Linq.Expressions;
using Utility.Helpers;
using Service.Helpers;
using Service.ApiModels.PaginatedList;

namespace Service.Services;

public class OrderService(IServiceProvider serviceProvider) : IOrderService
{
    private readonly IOrderRepository _orderRepository =
        serviceProvider.GetRequiredService<IOrderRepository>();
    private readonly IBaseRepository<OrderShop> _orderShopRepository =
        serviceProvider.GetRequiredService<IBaseRepository<OrderShop>>();
    private readonly IBaseRepository<Voucher> _voucherRepository =
        serviceProvider.GetRequiredService<IBaseRepository<Voucher>>();
    private readonly IBaseRepository<Product> _productRepository =
        serviceProvider.GetRequiredService<IBaseRepository<Product>>();
    private readonly IBaseRepository<Shop> _shopRepository =
        serviceProvider.GetRequiredService<IBaseRepository<Shop>>();
    private readonly IBaseRepository<Category> _categoryRepository =
        serviceProvider.GetRequiredService<IBaseRepository<Category>>();
    private readonly IUserRepository _userRepository =
        serviceProvider.GetRequiredService<IUserRepository>();
    private readonly MapperlyMapper _mapper =
        serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly ILogger _logger =
        serviceProvider.GetRequiredService<ILogger>();
    private readonly IUnitOfWork _unitOfWork =
        serviceProvider.GetRequiredService<IUnitOfWork>();
    private readonly IHttpContextAccessor _httpContextAccessor =
        serviceProvider.GetRequiredService<IHttpContextAccessor>();

    private const decimal PlatformRate = 0.15m;
    private const decimal ShipFee = 30000;
    private const decimal TaxRate = 0;

    public async Task<OrderRequest> CalculateTotalPrice(CartCalcRequest request)
    {
        _logger.Information("Calculate total price for order");

        // get all vouchers that shopId is null and those ids are in request.PlatformVouchers, Status is Active
        var voucherEntities = request.VoucherIds != null && request.VoucherIds.Any()
            ? await _voucherRepository.GetAllWithCondition(v => request.VoucherIds.Contains(v.Id)).ToListAsync()
            : null;

        if (request.VoucherIds != null && voucherEntities != null && voucherEntities.Count != request.VoucherIds.Count)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.NOT_EXISTED,
                                                             StatusCodes.Status400BadRequest);
        }

        if (voucherEntities != null)
        {
            foreach (var voucher in voucherEntities)
            {
                if (voucher.Status != VoucherStatusEnum.Active)
                {
                    throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                        ResponseMessageConstrantsVoucher.NOT_AVAILABLE,
                        StatusCodes.Status400BadRequest);
                }
            }
        }

        // get all products in request
        var cartItemIds = request.CartItems.Select(x => x.ProductId).ToList();
        var productEntities = await _productRepository.GetAllWithCondition(
            x => cartItemIds.Contains(x.Id) && x.DeletedTime == null,
            x => x.Shop).ToListAsync();

        if (!productEntities.Any())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsProduct.NOTFOUND,
                StatusCodes.Status404NotFound);
        }

        if (productEntities.Count != cartItemIds.Count)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsProduct.NOT_EXISTED,
                StatusCodes.Status400BadRequest);
        }

        // get all shops in request, group by shop id
        var shops = productEntities.GroupBy(x => x.ShopId).Select(x => x.First().Shop).ToList();

        // group products by shop, then calculate total price for each shop
        var cartShops = new List<CartShop>();
        var shopVoucher = new Voucher();
        var shopProductEntities = new List<Product>();
        var cartItems = new List<CartItem>();
        foreach (var shop in shops)
        {
            // get voucher for each shop
            if (voucherEntities != null && voucherEntities.Any())
            {
                shopVoucher = voucherEntities.FirstOrDefault(x => x.ShopId == shop.Id) ?? null;
                if (shopVoucher != null) voucherEntities.Remove(shopVoucher);
            }

            // get all products in shop
            shopProductEntities = productEntities.Where(x => x.ShopId == shop.Id).ToList();
            cartItems = _mapper.MapToCartItems(shopProductEntities);

            foreach (var cartItem in cartItems)
            {
                // map day rent and rent quantity to cart item
                var requestItem = request.CartItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (requestItem == null)
                {
                    throw new AppException(ResponseCodeConstants.NOT_FOUND,
                        ResponseMessageConstrantsProduct.NOTFOUND,
                        StatusCodes.Status404NotFound);
                }

                _mapper.MapRequestToCartItem(requestItem, cartItem);
                cartItem.CalculateSubTotal();
            }

            // create cart shop
            var cartShop = new CartShop
            {
                ShopId = shop.Id,
                ShopName = shop.ShopName,
                VoucherId = shopVoucher?.Id,
                ShopVoucher = shopVoucher,
                OrderDetails = cartItems
            };

            // calculate total price for each shop
            cartShop.CalculateShopTotal(PlatformRate, TaxRate, ShipFee);
            cartShops.Add(cartShop);
        }

        // calculate total price for all shops
        var cart = new Cart();
        cart.OrderShops = cartShops;

        // add platform voucher to cart
        if (voucherEntities != null && voucherEntities.Any(x => x.ShopId == null))
        {
            cart.PlatformVouchers.AddRange(voucherEntities.Where(x => x.ShopId == null));
        }

        cart.CalculateTotal(TaxRate, PlatformRate, cartShops);

        // map to order request
        var result = _mapper.MapToOrderRequest(cart);
        return result;
    }


    public Task<IList<OrderListResponse>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
    {
        _logger.Information("Get order by id {@orderId}", orderId);
        if (!await IsOrderExist(orderId))
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        var loginUserId = GetLoginUser().FindFirst(ClaimTypes.Sid);
        var order = await _orderRepository.GetOrderAsyncById(
            x => x.Id == orderId,
            false,
            false,
            true,
            true,
            true,
            x => x.OrderShops,
            x => x.Customer,
            x => x.PlatformVouchers);

        if (order.CustomerId != int.Parse(loginUserId.Value))
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseCodeConstants.FORBIDDEN,
                               StatusCodes.Status403Forbidden);
        }

        return _mapper.MapToOrderResponse(order);
    }

    public async Task<int> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        _logger.Information("Create order {@request}", request);
        var order = _mapper.MapToOrder(request);
        order.CustomerId = int.Parse(GetLoginUser().FindFirst(ClaimTypes.Sid).Value);

        // get all vouchers in request
        var shopVoucherIds = order.OrderShops.Where(x => x.VoucherId != null)
            .Select(x => x.VoucherId).ToList();
        var voucherIds = order.PlatformVouchers.Select(x => x.Id);
        var vouchers = await _voucherRepository.GetAllWithCondition(x =>
            voucherIds.Contains(x.Id) || shopVoucherIds.Contains(x.Id)).ToListAsync(cancellationToken);

        if (vouchers.Count != (voucherIds.Count() + shopVoucherIds.Count()))
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                ResponseMessageConstrantsVoucher.NOT_EXISTED,
                StatusCodes.Status400BadRequest);
        }

        // check if vouchers are existed or active
        foreach (var voucher in vouchers)
        {
            if (voucher.Status != VoucherStatusEnum.Active)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    ResponseMessageConstrantsVoucher.NOT_AVAILABLE,
                    StatusCodes.Status400BadRequest);
            }

            if (voucher.Limit.HasValue && voucher.Limit >= 1) voucher.Limit--;
            voucher.Used++;
            _voucherRepository.Update(voucher);
        }

        var platformVouchers = vouchers.Where(x => x.ShopId == null).ToList();
        order.PlatformVouchers = platformVouchers;
        _voucherRepository.TryAttachRange(order.PlatformVouchers);
        order = await _orderRepository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangeAsync();
        return order.Id;
    }

    public async Task<PaginatedListResponse<OrderShopListResponse>> GetAllOrderShopsAsync(OrderShopListRequest request)
    {
        _logger.Information("Get all order details for {@request}", request);
        // check if shop is existed and user is owner of shop
        var loginUser = GetLoginUser();
        Expression<Func<OrderShop, bool>> predicate = null;
        if (request.IsShop)
        {
            // if request is shop but user is not shop owner, not allowed
            if (loginUser.IsInRole("Customer"))
            {
                throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                    StatusCodes.Status403Forbidden);
            }

            var shopOwnerId = int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value);
            var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == shopOwnerId);
            if (shop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND,
                                       StatusCodes.Status404NotFound);
            }

            predicate = x => x.ShopId == shop.Id;
        }
        else
        {
            // if request is not shop but user is shop owner, not allowed
            if (loginUser.IsInRole("ShopOwner"))
            {
                throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsShop.NOTALLOWED,
                    StatusCodes.Status403Forbidden);
            }

            var customerId = int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value);
            predicate = x => x.Order.CustomerId == customerId;
        }

        // check if status is valid enum value then add to predicate
        if (request.Status != null
            && Enum.TryParse(request.Status.ToString(), out OrderShopStatusEnum parsedStatus)
            && Enum.IsDefined(typeof(OrderShopStatusEnum), parsedStatus))
        {
            // Combine the predicates using And with Helper.PredicateBuilder
            predicate = predicate.And(x => x.Status == parsedStatus);
        }

        var orderShop = await _orderShopRepository.GetAllPaginatedQueryable(
            request.PageNumber, request.PageSize,
            predicate,
            x => x.CreatedTime,
            x => x.Order,
            x => x.OrderDetails);
        return _mapper.MapToPaginatedListOrderShopListResponse(orderShop);
    }

    public async Task<OrderShopResponse> GetOrderShopsByIdAsync(int orderShopId)
    {
        _logger.Information("Get order detail by id {@orderShopId}", orderShopId);
        var loginUser = GetLoginUser();

        if (!await IsOrderShopExist(orderShopId))
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                               StatusCodes.Status404NotFound);
        }

        var orderShop = await _orderShopRepository.GetSingleAsync(x => x.Id == orderShopId,
            false,
            x => x.Order,
            x => x.Order.Customer,
            x => x.Shop,
            x => x.OrderDetails);

        if (loginUser.IsInRole("ShopOwner") &&
            orderShop.Shop.OwnerId != int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value))
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsShop.NOTALLOWED,
                                              StatusCodes.Status403Forbidden);
        }

        if (loginUser.IsInRole("Customer") &&
            orderShop.Order.CustomerId != int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value))
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                                                             StatusCodes.Status403Forbidden);
        }

        return _mapper.MapToOrderShopResponse(orderShop);
    }

    public async Task CancelOrderAsync(int orderId)
    {
        _logger.Information("Cancel order by id {@orderId}", orderId);
        if (!await IsOrderExist(orderId))
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                                              StatusCodes.Status404NotFound);
        }

        var loginUser = GetLoginUser();
        var userId = int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value);
        var order = await _orderRepository.GetSingleAsync(x => x.Id == orderId,
            false,
            x => x.OrderShops,
            x => x.PlatformVouchers);

        if (order.CustomerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                                              StatusCodes.Status403Forbidden);
        }

        if (order.PaymentTime != null)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED,
                                                             StatusCodes.Status400BadRequest);
        }

        foreach (var orderShop in order.OrderShops)
        {
            var processor = new OrderShopProcessor(orderShop);
            processor.Cancel();
            orderShop.CancelBy = userId;
            _orderShopRepository.Update(orderShop);
        }

        await _unitOfWork.SaveChangeAsync();
    }

    public async Task ChangeOrderStatus(int orderShopId)
    {
        _logger.Information("Change order status by id {@orderShopId}", orderShopId);
        if (!await IsOrderShopExist(orderShopId))
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                                                             StatusCodes.Status404NotFound);
        }

        var loginUser = GetLoginUser();
        var orderShop = await _orderShopRepository.GetSingleAsync(x => x.Id == orderShopId
            && (x.Status != OrderShopStatusEnum.Cancelled || x.Status != OrderShopStatusEnum.Rejected),
            false,
            x => x.Order);

        // customer can only change status of their own order and order status must be in Shipping or Received
        if (orderShop.Status is OrderShopStatusEnum.Shipping or OrderShopStatusEnum.Received
           && loginUser.IsInRole("Customer")
           && orderShop.Order.CustomerId != int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value))
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                                                                            StatusCodes.Status403Forbidden);
        }

        if (orderShop.Status is not (OrderShopStatusEnum.Shipping or OrderShopStatusEnum.Received)
            && loginUser.IsInRole("ShopOwner")
            && orderShop.Shop.OwnerId != int.Parse(loginUser.FindFirst(ClaimTypes.Sid).Value))
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                                                                                           StatusCodes.Status403Forbidden);
        }

        var processor = new OrderShopProcessor(orderShop);
        processor.ChangeNextStatus();
        _orderShopRepository.Update(orderShop);

        await _unitOfWork.SaveChangeAsync();
    }

    private ClaimsPrincipal? GetLoginUser()
    {
        return _httpContextAccessor.HttpContext.User;
    }

    private async Task<bool> IsOrderExist(int orderId)
    {
        return await _orderRepository.IsExistAsync(x => x.Id == orderId);
    }

    private async Task<bool> IsOrderShopExist(int orderShopId)
    {
        return await _orderShopRepository.IsExistAsync(x => x.Id == orderShopId);
    }
}
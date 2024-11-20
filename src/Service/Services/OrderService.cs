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
using Flurl.Util;
using Utility.Helpers;
using Service.Helpers;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;
using Service.Utils;

namespace Service.Services;

public class OrderService(IServiceProvider serviceProvider) : IOrderService
{
    private readonly IOrderRepository _orderRepository =
        serviceProvider.GetRequiredService<IOrderRepository>();
    private readonly IOrderShopRepository _orderShopRepository =
        serviceProvider.GetRequiredService<IOrderShopRepository>();
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

    public async Task<PaginatedListResponse<OrderListResponse>> GetAllOrdersAsync(OrderListRequest request)
    {
        _logger.Information("Get all orders");
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(loginedUser);
        var userId = JwtClaimUltils.GetUserId(loginedUser);

        Expression<Func<Order, bool>> predicate = null;
        if (role.Contains(UserRoleEnum.Customer.ToString()))
        {
            predicate = x => x.CustomerId == userId;
        }

        var orders = await _orderRepository.GetAllPaginatedQueryable(
                       request.PageNumber, request.PageSize,
                                  predicate,
                                  x => x.CreatedTime,
                       x => x.Customer
                       );

        return _mapper.MapToOrderListResponse(orders);
    }

    public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
    {
        _logger.Information("Get order by id {@orderId}", orderId);
        var user = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(user);
        var loginUserId = JwtClaimUltils.GetUserId(JwtClaimUltils.GetLoginedUser(_httpContextAccessor));
        var order = await _orderRepository.GetOrderByIdAsync(
            x => x.Id == orderId,
            false,
            false,
            true,
            true,
            true,
            x => x.OrderShops,
            x => x.Customer,
            x => x.PlatformVouchers);

        if (order == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        if (role.Contains(UserRoleEnum.Admin.ToString()))
        {
            return _mapper.MapToOrderResponse(order);
        }

        // only customer can view their own order
        ValidateCustomerOrder(role, order.CustomerId, loginUserId);

        return _mapper.MapToOrderResponse(order);
    }

    public async Task<int> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        _logger.Information("Create order {@request}", request);

        // Map order request to order entity
        var order = _mapper.MapToOrder(request);
        order.CustomerId = JwtClaimUltils.GetUserId(JwtClaimUltils.GetLoginedUser(_httpContextAccessor));
        var shopId = await _shopRepository.GetSingleAsync(x => x.OwnerId == order.CustomerId);

        // check if orderShop contains shopId
        if (order.OrderShops.Any(x => x.ShopId != shopId.Id))
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                ResponseMessageConstrantsOrder.CUSTOMER_NOTALLOWED_ORDER + $" ShopId: {shopId}",
                               StatusCodes.Status400BadRequest);
        }

        // Combine all voucher IDs (platform vouchers + shop vouchers), excluding null or zero values
        var allVoucherIds = order.PlatformVouchers.Select(x => x.Id)
            .Concat(order.OrderShops.Where(x => x.VoucherId.HasValue && x.VoucherId.Value != 0)
                .Select(x => x.VoucherId.Value))
            .Distinct()
            .ToList();

        // Fetch vouchers from repository
        var vouchers = await _voucherRepository.GetAllWithCondition(x =>
                x.DeletedTime == null && allVoucherIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        // Validate the number of vouchers fetched
        if (vouchers.Count != allVoucherIds.Count)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                ResponseMessageConstrantsVoucher.NOT_EXISTED,
                StatusCodes.Status400BadRequest);
        }

        // Check if vouchers are active and update usage
        foreach (var voucher in vouchers)
        {
            if (voucher.Status != VoucherStatusEnum.Active)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                    ResponseMessageConstrantsVoucher.NOT_AVAILABLE + $" VoucherId: {voucher.Id}",
                    StatusCodes.Status400BadRequest);
            }

            // Update voucher usage limits
            if (voucher.Limit.HasValue && voucher.Limit.Value >= 1)
            {
                voucher.Limit--;
            }

            voucher.Used++;
            _voucherRepository.Update(voucher);
        }

        // Attach platform vouchers to the order
        var platformVouchers = vouchers.Where(x => x.ShopId == null).ToList();
        order.PlatformVouchers = platformVouchers;
        _voucherRepository.TryAttachRange(order.PlatformVouchers);

        // Add order to repository and save changes
        order = await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangeAsync();

        return order.Id;
    }

    public OrderShopStatusEnumResponse GetAllOrderShopStatusEnum()
    {
        _logger.Information("Get all order shop status enum");
        var orderShopStatusEnum = EnumHelper.GetEnumDictionary<OrderShopStatusEnum>();
        return new OrderShopStatusEnumResponse
        {
            OrderShopStatusEnums = orderShopStatusEnum
        };
    }

    public async Task<PaginatedListResponse<OrderShopListResponse>> GetAllOrderShopsAsync(OrderShopListRequest request)
    {
        _logger.Information("Get all order shops for {@request}", request);
        // check if shop is existed and user is owner of shop
        var loginUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(loginUser);
        var userId = JwtClaimUltils.GetUserId(loginUser);
        Expression<Func<OrderShop, bool>> predicate = null;

        // if user is not admin, check if user is shop owner or customer and get ordershop by shop id or customer id
        if (!role.Contains(UserRoleEnum.Admin.ToString()))
        {
            if (request.IsShop)
            {
                // if request is shop but user is not shop owner, not allowed
                if (!role.Contains(UserRoleEnum.ShopOwner.ToString()))
                {
                    throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.NOTALLOWED,
                        StatusCodes.Status403Forbidden);
                }

                // get ordershop by shop id
                var shop = await _shopRepository.GetSingleAsync(x => x.OwnerId == userId);
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
                if (!role.Contains(UserRoleEnum.Customer.ToString()))
                {
                    throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsShop.NOTALLOWED,
                        StatusCodes.Status403Forbidden);
                }

                // get ordershop by customer id
                predicate = x => x.Order.CustomerId == userId;
            }
        }

        // check if status is valid enum value then add to predicate
        if (request.Status != null
            && Enum.TryParse(request.Status.ToString(), out OrderShopStatusEnum parsedStatus)
            && Enum.IsDefined(typeof(OrderShopStatusEnum), parsedStatus))
        {
            // Combine the predicates using And with Helper.PredicateBuilder
            predicate = predicate.And(x => x.Status == parsedStatus);
        }

        var orderShop = await _orderShopRepository.GetAllOrderShopsAsync(
            request.PageNumber, request.PageSize,
            predicate, false, true,
            x => x.CreatedTime,
            x => x.Order,
            x =>x.Shop,
            x => x.OrderDetails);
        return _mapper.MapToPaginatedListOrderShopListResponse(orderShop);
    }

    public async Task<OrderShopResponse> GetOrderShopsByIdAsync(int orderShopId, bool isShop)
    {
        _logger.Information("Get order shop by id {@orderShopId}", orderShopId);

        var loginUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(loginUser);
        var userId = JwtClaimUltils.GetUserId(loginUser);

        var orderShop = await _orderShopRepository.GetOrderShopByIdAsync(x => x.Id == orderShopId,
            false, true,
            x => x.Order,
            x => x.Order.Customer,
            x => x.Shop,
            x => x.OrderDetails);

        if (role.Contains(UserRoleEnum.Admin.ToString()))
        {
            return _mapper.MapToOrderShopResponse(orderShop);
        }

        if (isShop)
        {
            ValidateShopOwnerOrderShop(role, orderShop, userId, MethodEnum.GET);
        }
        else
        {
            ValidateCustomerOrderShop(role, orderShop, userId, MethodEnum.GET);
        }

        return _mapper.MapToOrderShopResponse(orderShop);
    }

    public async Task CancelOrderAsync(int orderId)
    {
        _logger.Information("Cancel order by id {@orderId}", orderId);
        var userId = JwtClaimUltils.GetUserId(JwtClaimUltils.GetLoginedUser(_httpContextAccessor));
        var order = await _orderRepository.GetSingleAsync(
            x => x.Id == orderId,
            false,
            x => x.OrderShops,
            x => x.PlatformVouchers);

        if (order == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        // customer can only cancel their own order
        if (order.CustomerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.CUSTOMER_NOTALLOWED,
                                              StatusCodes.Status403Forbidden);
        }

        // order must be in WaitForPayment status
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

    public async Task ChangeOrderShopStatusAsync(int orderShopId, bool isShop)
    {
        _logger.Information("Change order status by id {@orderShopId}", orderShopId);
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(loginedUser);
        var userId = JwtClaimUltils.GetUserId(loginedUser);

        var orderShop = await _orderShopRepository.GetSingleAsync(x => x.Id == orderShopId
            && (x.Status != OrderShopStatusEnum.Cancelled || x.Status != OrderShopStatusEnum.Rejected),
            false,
            x => x.Order,
            x => x.Shop);

        if (orderShop == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND,
                                                                                                        StatusCodes.Status404NotFound);
        }

        if (isShop)
        {
            ValidateShopOwnerOrderShop(role, orderShop, userId, MethodEnum.PUT);
        }
        else
        {
            ValidateCustomerOrderShop(role, orderShop, userId, MethodEnum.PUT);
        }

        var processor = new OrderShopProcessor(orderShop);
        processor.ChangeNextStatus();
        _orderShopRepository.Update(orderShop);

        await _unitOfWork.SaveChangeAsync(_httpContextAccessor);
    }

    public async Task CancelOrderShopAsync(int orderShopId)
    {
        _logger.Information("Cancel order shop by id {@orderShopId}", orderShopId);
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var role = JwtClaimUltils.GetUserRole(loginedUser);
        var userId = JwtClaimUltils.GetUserId(loginedUser);

        var orderShop = await _orderShopRepository.GetSingleAsync(x => x.Id == orderShopId,
                       false,
                                  x => x.Order,
                       x => x.Shop
                       );

        if (orderShop == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, 
                ResponseMessageConstrantsOrder.NOT_FOUND, 
                StatusCodes.Status404NotFound);
        }

        // order must be in WaitForPayment status
        if (orderShop.Status is not OrderShopStatusEnum.WaitForPayment)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED,
                StatusCodes.Status400BadRequest);
        }

        
        if (role.Contains(UserRoleEnum.ShopOwner.ToString())
            && orderShop.Shop.OwnerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, 
                ResponseMessageConstrantsOrder.SHOPOWNER_NOTALLOWED,
                StatusCodes.Status403Forbidden);
        }

        var processor = new OrderShopProcessor(orderShop);
        processor.Cancel();
        orderShop.CancelBy = userId;
        _orderShopRepository.Update(orderShop);

        await _unitOfWork.SaveChangeAsync(_httpContextAccessor);
    }

    private async Task<bool> IsOrderExist(int orderId)
    {
        return await _orderRepository.IsExistAsync(x => x.Id == orderId);
    }

    private async Task<bool> IsOrderShopExist(int orderShopId)
    {
        return await _orderShopRepository.IsExistAsync(x => x.Id == orderShopId);
    }
    
    public async Task ChangeToWaitingForRefund(int id)
    {
        var orderShop = await _orderShopRepository.GetByIdAsync(id);
        if (orderShop == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsOrder.NOT_FOUND, StatusCodes.Status404NotFound);
        }

        orderShop.Status = OrderShopStatusEnum.WaitingForRefund;
        _orderShopRepository.Update(orderShop);
        await _unitOfWork.SaveChangeAsync();
    }

    private void ValidateCustomerOrderShop(IList<string> role, OrderShop orderShop, int userId, MethodEnum method)
    {
        // customer can only update their own order
        if (role.Contains(UserRoleEnum.Customer.ToString()) && orderShop.Order.CustomerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.CUSTOMER_NOTALLOWED,
                StatusCodes.Status403Forbidden);
        }

        // customer can only update when ordershop status is Shipping or Received
        if (method == MethodEnum.PUT &&
            orderShop.Status is not (OrderShopStatusEnum.Shipping or OrderShopStatusEnum.Received))
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED,
                StatusCodes.Status400BadRequest);
        }

        // customer can only cancel when ordershop status is WaitForPayment
        if (method == MethodEnum.DELETE &&
            orderShop.Status != OrderShopStatusEnum.WaitForPayment)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED,
                               StatusCodes.Status400BadRequest);
        }
    }

    private void ValidateShopOwnerOrderShop(IList<string> role, OrderShop orderShop, int userId, MethodEnum method)
    {
        // shop owner can only update their own order
        if (role.Contains(UserRoleEnum.ShopOwner.ToString()) && orderShop.Shop.OwnerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.SHOPOWNER_NOTALLOWED,
                               StatusCodes.Status403Forbidden);
        }

        // shop owner can only update when ordershop status is Pending or Approved or Returning or Returned
        if (method == MethodEnum.PUT &&
            orderShop.Status is not (OrderShopStatusEnum.Pending or OrderShopStatusEnum.Approved
                or OrderShopStatusEnum.Returning or OrderShopStatusEnum.Returned))
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED,
                                              StatusCodes.Status400BadRequest);
        }
    }

    private void ValidateCustomerOrder(IList<string> role, int customerId, int userId)
    {
        // customer can only update their own order
        if (role.Contains(UserRoleEnum.Customer.ToString()) && customerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.CUSTOMER_NOTALLOWED,
                               StatusCodes.Status403Forbidden);
        }
    }

    private void ValidateShopOwnerOrder(IList<string> role, int shopOwnerId, int userId)
    {
        // shop owner can only update their own order
        if (role.Contains(UserRoleEnum.ShopOwner.ToString()) && shopOwnerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsOrder.SHOPOWNER_NOTALLOWED,
                                              StatusCodes.Status403Forbidden);
        }
    }
}
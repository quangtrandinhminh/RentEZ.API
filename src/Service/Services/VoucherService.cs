using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;
using Service.Interfaces;
using Service.Mapper;
using System.Linq.Expressions;
using Repository.Interfaces;
using Service.Helpers;
using Utility.Constants;
using Utility.Exceptions;
using Utility.Helpers;
using ILogger = Serilog.ILogger;
using Service.Utils;
using Utility.Enum;

namespace Service.Services;

public class VoucherService(IServiceProvider serviceProvider) : IVoucherService
{
    private readonly IBaseRepository<Voucher> _voucherRepository = serviceProvider
        .GetRequiredService<IBaseRepository<Voucher>>();
    private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
    private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
    private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    private readonly MapperlyMapper _mapper = serviceProvider
        .GetRequiredService<MapperlyMapper>();
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
    private readonly IHttpContextAccessor _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

    public VoucherEnumResponse GetAllVoucherEnum()
    {
        // Get all voucher types and value types, make them a idictionary with key-value
        var voucherTypeEnum = EnumHelper.GetEnumDictionary<VoucherTypeEnum>();
        var voucherValueTypeEnum = EnumHelper.GetEnumDictionary<VoucherValueTypeEnum>();
        var voucherStatusEnum = EnumHelper.GetEnumDictionary<VoucherStatusEnum>();
        return new VoucherEnumResponse
        {
            VoucherTypeEnums = voucherTypeEnum,
            VoucherValueTypeEnums = voucherValueTypeEnum,
            VoucherStatusEnums = voucherStatusEnum
        };
    }
    public async Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(VoucherQueryRequest request)
    {
        _logger.Information("Get all voucher request {@request}", request);

        var predicate = VoucherPredicateBuilder(request);
        var vouchers = await _voucherRepository.GetAllPaginatedQueryable(
            request.PageNumber, request.PageSize, predicate, includeProperties: v => v.Users);

        // filter voucher by status 
        if (request.Status != null)
        {
            vouchers.Items = vouchers.Items.Where(v => v.Status == request.Status).ToList();
        }

        // if not found any voucher
        if (vouchers == null || !vouchers.Items.Any())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsVoucher.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        var voucherResponses = _mapper.MapToPaginatedListVoucherResponse(vouchers);
        return voucherResponses;
    }

    public async Task AddVoucherToCustomer(int voucherId)
    {
        var userId = JwtClaimUltils.GetUserId(JwtClaimUltils.GetLoginedUser(_httpContextAccessor));
        _logger.Information("Add voucher to customer {@voucherId} {@userId}", voucherId, userId);
        var voucher = await GetVoucherEntityByIdAsync(voucherId);
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND,
                               StatusCodes.Status404NotFound);
        }
        voucher.Users.Add(user);
        _voucherRepository.Update(voucher);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<VoucherResponse> GetVoucherByIdAsync(int voucherId)
    {
        _logger.Information("Get voucher by id {@voucherId}", voucherId);
        var voucher = await GetVoucherEntityByIdAsync(voucherId);
        return _mapper.MapToVoucherResponse(voucher);
    }

    public async Task<VoucherResponse> CreateVoucherAsync(VoucherRequest request, CancellationToken cancellationToken = default)
    {
        _logger.Information("Create voucher {@request}", request);
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var userId = JwtClaimUltils.GetUserId(loginedUser);
        var roles = JwtClaimUltils.GetUserRole(loginedUser);
        
        ValidVoucherRequest(request);
        var voucher = _mapper.MapToVoucher(request);
        if (roles.Contains(UserRoleEnum.ShopOwner.ToString()))
        {
            var shop = await _shopRepository.GetSingleAsync(s => s.OwnerId == userId);
            voucher.ShopId = shop.Id;
        }

        voucher = await _voucherRepository.AddAsync(voucher, cancellationToken);
        await _unitOfWork.SaveChangeAsync(_httpContextAccessor);
        var response = _mapper.MapToVoucherResponse(voucher);
        return response;
    }

    public async Task UpdateVoucherAsync(VoucherOrderRequest request)
    {
        _logger.Information("Update request {@request}", request);
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var userId = JwtClaimUltils.GetUserId(loginedUser);
        var roles = JwtClaimUltils.GetUserRole(loginedUser);
        
        ValidVoucherRequest(request);
        var voucher = await GetVoucherEntityByIdAsync(request.Id);

        // admin have permission to update all voucher
        // shop owner try to update other shop voucher, forbidden
        if (roles.Contains(UserRoleEnum.ShopOwner.ToString() ) 
            && voucher.ShopId != null && voucher.Shop?.OwnerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsShop.NOTALLOWED,
                                                             StatusCodes.Status403Forbidden);
        }

        _mapper.MapToVoucher(request, voucher);
        _voucherRepository.Update(voucher);
        await _unitOfWork.SaveChangeAsync(_httpContextAccessor);
    }

    public async Task DeleteVoucherAsync(int voucherId)
    {
        _logger.Information("Delete voucher {@voucherId}", voucherId);
        var loginedUser = JwtClaimUltils.GetLoginedUser(_httpContextAccessor);
        var userId = JwtClaimUltils.GetUserId(loginedUser);
        var roles = JwtClaimUltils.GetUserRole(loginedUser);
        var voucher = await GetVoucherEntityByIdAsync(voucherId);

        // admin have permission to delete all voucher
        // shop owner try to update other shop voucher, forbidden
        if (roles.Contains(UserRoleEnum.ShopOwner.ToString()) 
            && voucher.ShopId != null && voucher.Shop?.OwnerId != userId)
        {
            throw new AppException(ResponseCodeConstants.FORBIDDEN, ResponseMessageConstrantsShop.NOTALLOWED,
                StatusCodes.Status403Forbidden);
        }

        voucher.DeletedTime = CoreHelper.SystemTimeNow;
        _voucherRepository.Update(voucher);
        await _unitOfWork.SaveChangeAsync();
    }

    private async Task<Voucher> GetVoucherEntityByIdAsync(int voucherId)
    {
        var voucher = await _voucherRepository.GetSingleAsync(x => x.Id == voucherId, includeProperties: x => x.Shop);
        if (voucher == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsVoucher.NOT_FOUND,
                               StatusCodes.Status404NotFound);
        }
        return voucher;
    }

    private Expression<Func<Voucher, bool>> VoucherPredicateBuilder(VoucherQueryRequest request)
    {
        // Not need token, just narrow down the query
        Expression<Func<Voucher, bool>> predicate = v => v.DeletedTime == null;
        if (request.Type != null)
        {
            predicate = predicate.And(v => (int)v.Type == request.Type);
        }

        if (request.ValueType != null)
        {
            predicate = predicate.And(v => (int)v.ValueType == request.ValueType);
        }

        if (request.CustomerId != null)
        {
            predicate = predicate.And(v => v.Users.Select(u => u.Id).Contains<int>(request.CustomerId.Value));
        }

        if (request.ShopId != null)
        {
            predicate = predicate.And(v => v.ShopId == request.ShopId);
        }

        return predicate;
    }

    private void ValidVoucherRequest(VoucherRequest request)
    {
        // value > 0
        if (request.Value <= 0)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.VALUE_INVALID,
                                              StatusCodes.Status400BadRequest);
        }

        // limit > 0
        if (request.Limit != null && request.Limit < 0)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.LIMIT_INVALID,
                               StatusCodes.Status400BadRequest);
        }

        // if min max value is not null, min, max > 0
        if (request.MinRentValue != null && request.MinRentValue < 0)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.MIN_RENTVALUE_INVALID,
                                              StatusCodes.Status400BadRequest);
        }

        if (request.MaxDiscountValue != null && request.MaxDiscountValue < 0)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.MAX_DISVALUE_INVALID,
                StatusCodes.Status400BadRequest);
        }

        // value type is percentage, value <= 1
        if (request.ValueType == VoucherValueTypeEnum.Percent && request.Value > 1)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.VALUE_INVALID,
                               StatusCodes.Status400BadRequest);
        }

        if (request.ValueType == VoucherValueTypeEnum.Amount && request.MaxDiscountValue.HasValue)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.MAX_DISCOUNT_INVALID,
                                              StatusCodes.Status400BadRequest);
        }

        if (request is { StartDate: not null, EndDate: not null } && request.StartDate > request.EndDate)
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsVoucher.EXPIRED_TIME_INVALID,
                               StatusCodes.Status400BadRequest);
        }
    }
}
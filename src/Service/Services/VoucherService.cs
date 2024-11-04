using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;
using Service.Interfaces;
using Service.Mapper;
using System.Linq.Expressions;
using Repository.Extensions;
using Repository.Interfaces;
using Service.Helpers;
using Utility.Constants;
using Utility.Exceptions;
using Utility.Helpers;
using ILogger = Serilog.ILogger;

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

    public async Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(VoucherQueryRequest request)
    {
        _logger.Information("Get all voucher request {@request}", request);

        var predicate = await VoucherPredicateBuilder(request);
        var vouchers = await _voucherRepository.GetAllPaginatedQueryable(
            request.PageNumber, request.PageSize, predicate, includeProperties: v => v.Users);

        if (vouchers == null || !vouchers.Items.Any())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsVoucher.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        var voucherResponses = _mapper.MapToPaginatedListVoucherResponse(vouchers);
        return voucherResponses;
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
        var voucher = _mapper.MapToVoucher(request);
        var response = _mapper.MapToVoucherResponse(await _voucherRepository.AddAsync(voucher, cancellationToken));
        await _unitOfWork.SaveChangeAsync();
        return response;
    }

    public async Task UpdateVoucherAsync(VoucherOrderRequest request)
    {
        _logger.Information("Update request {@request}", request);
        var voucher = await GetVoucherEntityByIdAsync(request.Id);
        _mapper.MapToVoucher(request, voucher);
        _voucherRepository.Update(voucher);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task DeleteVoucherAsync(int voucherId)
    {
        _logger.Information("Delete voucher {@voucherId}", voucherId);
        var voucher = await GetVoucherEntityByIdAsync(voucherId);
        voucher.DeletedTime = CoreHelper.SystemTimeNow;
        _voucherRepository.Update(voucher);
        await _unitOfWork.SaveChangeAsync();
    }

    private async Task<Voucher> GetVoucherEntityByIdAsync(int voucherId)
    {
        var voucher = await _voucherRepository.GetSingleAsync(x => x.Id == voucherId);
        if (voucher == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsVoucher.NOT_FOUND,
                               StatusCodes.Status404NotFound);
        }
        return voucher;
    }

    private async Task<Expression<Func<Voucher, bool>>> VoucherPredicateBuilder(VoucherQueryRequest request)
    {
        Expression<Func<Voucher, bool>> predicate = v => v.DeletedTime == null;
        if (request.Type != null)
        {
            predicate = predicate.And(v => (int)v.Type == request.Type);
        }

        if (request.ValueType != null)
        {
            predicate = predicate.And(v => (int)v.ValueType == request.ValueType);
        }

        if (request.UserId != null)
        {
            var user = await _userRepository.GetSingleAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND,
                    StatusCodes.Status404NotFound);
            }
            predicate = predicate.And(v => v.Users.Select(u => u.Id).Contains<>(request.UserId));
        }

        if (request.ShopId != null)
        {
            await _shopRepository.IsExistAsync(x => x.Id == request.ShopId);
            predicate = predicate.And(v => v.ShopId == request.ShopId);
        }

        return predicate;
    }
}
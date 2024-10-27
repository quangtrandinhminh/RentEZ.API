using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;
using Service.Interfaces;
using Service.Mapper;
using Utility.Constants;
using Utility.Exceptions;

namespace Service.Services;

public class VoucherService(IServiceProvider serviceProvider) : IVoucherService
{
    private readonly IBaseRepository<Voucher> _voucherRepository = serviceProvider
        .GetRequiredService<IBaseRepository<Voucher>>();

    private readonly MapperlyMapper _mapper = serviceProvider
        .GetRequiredService<MapperlyMapper>();

    public async Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(int pageNumber, int pageSize)
    {
        var vouchers = await _voucherRepository.GetAllPaginatedQueryable(
            pageNumber, pageSize,
            v => v.DeletedTime == null);

        if (vouchers == null || !vouchers.Items.Any())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsVoucher.NOT_FOUND,
                StatusCodes.Status404NotFound);
        }

        var voucherResponses = _mapper.MapToPaginatedListVoucherResponse(vouchers);
        return voucherResponses;
    }

    public Task<Voucher> GetVoucherByIdAsync(int voucherId)
    {
        throw new NotImplementedException();
    }

    public Task<Voucher> CreateVoucherAsync(Voucher voucher)
    {
        throw new NotImplementedException();
    }

    public Task<Voucher> UpdateVoucherAsync(Voucher voucher)
    {
        throw new NotImplementedException();
    }

    public Task DeleteVoucherAsync(int voucherId)
    {
        throw new NotImplementedException();
    }
}
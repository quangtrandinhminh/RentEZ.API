using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;
using Utility.Enum;

namespace Service.Interfaces;

public interface IVoucherService
{
    VoucherEnumResponse GetAllVoucherEnum();
    Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(VoucherQueryRequest request);
    Task<VoucherResponse> GetVoucherByIdAsync(int voucherId);

    Task<VoucherResponse> CreateVoucherAsync(VoucherRequest request, CancellationToken cancellationToken = default);
    Task UpdateVoucherAsync(VoucherOrderRequest request);
    Task DeleteVoucherAsync(int voucherId);
}
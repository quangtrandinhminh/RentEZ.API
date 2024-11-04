using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;

namespace Service.Interfaces;

public interface IVoucherService
{
    // get all voucher, by type, by value, by min value, by max value, user id, by shop id
    Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(VoucherQueryRequest request);

    // get voucher by id
    Task<VoucherResponse> GetVoucherByIdAsync(int voucherId);

    // create voucher
    Task<VoucherResponse> CreateVoucherAsync(VoucherRequest request, CancellationToken cancellationToken = default);

    // update voucher
    Task UpdateVoucherAsync(VoucherOrderRequest request);

    // delete voucher
    Task DeleteVoucherAsync(int voucherId);
}
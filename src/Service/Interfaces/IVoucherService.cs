using Repository.Models;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.Voucher;

namespace Service.Interfaces;

public interface IVoucherService
{
    // get all voucher, by type, by value, by min value, by max value, user id, by shop id
    Task<PaginatedListResponse<VoucherResponse>> GetAllVoucherAsync(int pageNumber, int pageSize);

    // get voucher by id
    Task<Voucher> GetVoucherByIdAsync(int voucherId);

    // create voucher
    Task<Voucher> CreateVoucherAsync(Voucher voucher);

    // update voucher
    Task<Voucher> UpdateVoucherAsync(Voucher voucher);

    // delete voucher
    Task DeleteVoucherAsync(int voucherId);
}
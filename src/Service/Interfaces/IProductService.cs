using Service.ApiModels.PaginatedList;
using Service.ApiModels.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedListResponse<ProductListResponse>> GetAllProducts(ProductListRequest request);
        Task<ProductResponse> GetProductById(int id);
        Task CreateProduct(ProductCreateRequest productRequest, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductCreateRequest productRequest, int id);
        Task DeleteProductAsync(int id);
        Task<List<ProductListResponse>> GetOwnProductAsync();
    }
}

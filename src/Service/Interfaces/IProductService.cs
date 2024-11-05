using Service.ApiModels.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductListResponse>> GetAllProducts(int? categoryId = null, int? shopId = null);
        Task<ProductResponse> GetProductById(int id);
        Task CreateProduct(ProductCreateRequest productRequest, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductCreateRequest productRequest, int id);
        Task DeleteProductAsync(int id);
    }
}

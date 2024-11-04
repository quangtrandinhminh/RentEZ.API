using Service.ApiModels.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts(int? categoryId = null);
        Task<ProductResponse> GetProductById(int id);
        Task CreateProduct(ProductCreateRequest productRequest, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductCreateRequest productRequest, int id);
        Task DeleteProductAsync(int id);
    }
}

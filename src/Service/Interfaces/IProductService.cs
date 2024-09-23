using Service.Models.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts(int? categoryId = null);
        Task<ProductResponse> GetProductById(int id);
        Task CreateProduct(ProductCreateRequestDto productRequest, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductCreateRequestDto productRequest, int id);
        Task DeleteProductAsync(int id);
    }
}

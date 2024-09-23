using BusinessObject.DTO.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProducts(int? categoryId = null);
        Task<ProductResponseDto> GetProductById(int id);
        Task CreateProduct(ProductCreateRequestDto productRequest, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductCreateRequestDto productRequest, int id);
        Task DeleteProductAsync(int id);
    }
}

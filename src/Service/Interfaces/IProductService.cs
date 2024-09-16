using BusinessObject.DTO.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProducts();
        Task<ProductResponseDto> GetProductById(int id);
    }
}

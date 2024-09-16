using Service.Models.Product;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(int id);
    }
}

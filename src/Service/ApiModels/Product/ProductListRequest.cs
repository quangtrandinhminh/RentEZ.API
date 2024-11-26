using Service.ApiModels.PaginatedList;

namespace Service.ApiModels.Product
{
    public class ProductListRequest : PaginatedListRequest
    {
        public string? SearchTerm { get; set; }
        public int? categoryId { get; set; } = null;
        public int? shopId { get; set; } = null;
    }
}

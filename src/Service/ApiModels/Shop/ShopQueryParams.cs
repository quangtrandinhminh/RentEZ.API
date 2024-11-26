using Service.ApiModels.PaginatedList;

namespace Service.ApiModels.Shop
{
    public class ShopQueryParams : PaginatedListRequest
    {
        public string? SearchTerm { get; set; }
        public bool? IsVerified { get; set; }
    }
}

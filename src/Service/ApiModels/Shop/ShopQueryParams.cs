using Service.ApiModels.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiModels.Shop
{
    public class ShopQueryParams : PaginatedListRequest
    {
        public string? SearchTerm { get; set; }
        public bool? IsVerified { get; set; }
    }
}

using Service.ApiModels.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiModels.Product
{
    public class ProductListRequest : PaginatedListRequest
    {
        public int? categoryId { get; set; } = null;
        public int? shopId { get; set; } = null;
    }
}

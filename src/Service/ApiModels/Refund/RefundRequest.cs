using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiModels.Refund
{
    public class RefundRequest
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ApiModels.Refund
{
    public class VietQRRequest
    {
        public decimal Amount { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public int BankId { get; set; }
        public string Description { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Service.ApiModels.Refund;
using Service.ApiModels.VNPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRefundService
    {
        Task<(string customerRefundQR, string shopPaymentQR)> ProcessRefundAsync(RefundRequest request);
    }
}

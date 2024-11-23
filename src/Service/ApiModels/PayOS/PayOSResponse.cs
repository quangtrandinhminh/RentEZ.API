using Net.payOS.Types;

namespace Service.ApiModels.PayOS;

public class PayOSResponse
{
    public String? Code { get; set; }
    public String? Desc { get; set; }
    public PaymentLinkInformation? Data { get; set; }
    public String? Signature { get; set; }
}
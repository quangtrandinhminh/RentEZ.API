using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.User;

public class BankInfoRequest
{
    public int? BankId { get; set; }

    [MaxLength(19)]
    public String? AccountNo { get; set; }

    [MaxLength(255)]
    public String? AccountName { get; set; }
}
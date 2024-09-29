using System.ComponentModel.DataAnnotations;

namespace Service.Models.User;

public class ResendEmailRequest
{
    [Required]
    public string UserName { get; set; }
}
using Utility.Enum;

namespace Service.Models;

public class SendMailModel
{
    public string Name { get; set; }
    public string Token { get; set; }
    public MailTypeEnum Type { get; set; }
    public string Email { get; set; }
}
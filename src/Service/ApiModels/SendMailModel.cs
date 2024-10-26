using Utility.Enum;

namespace Service.ApiModels;

public class SendMailModel
{
    public string Name { get; set; }
    public string Token { get; set; }
    public MailTypeEnum Type { get; set; }
    public string Email { get; set; }
}
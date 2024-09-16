using Service.Models;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        void SendMail(SendMailModel model);
    }
}
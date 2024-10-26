using Service.ApiModels;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        void SendMail(SendMailModel model);
    }
}
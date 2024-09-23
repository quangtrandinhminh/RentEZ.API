﻿using Service.Interfaces;
using System.Net;
using System.Net.Mail;
using Utility.Config;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Service.Models;

namespace Service.Services
{
    public class EmailService(IServiceProvider serviceProvider) : IEmailService
    {
        private string EMAIL_SENDER = MailSettingModel.Instance.FromAddress;
        private string EMAIL_SENDER_PASSWORD = MailSettingModel.Instance.Smtp.Password;
        private string EMAIL_SENDER_HOST = MailSettingModel.Instance.Smtp.Host;
        private int EMAIL_SENDER_PORT = Convert.ToInt16(MailSettingModel.Instance.Smtp.Port);
        private bool EMAIL_IsSSL = Convert.ToBoolean(MailSettingModel.Instance.Smtp.EnableSsl);

        public void SendMail(SendMailModel model)
        {
            switch (model.Type)
            {
                case MailTypeEnum.Verify:
                    CreateVerifyMail(model);
                    break;
                case MailTypeEnum.ResetPassword:
                    CreateResetPassMail(model);
                    break;
                default:
                    break;
            }
        }

        private void CreateVerifyMail(SendMailModel model)
        {
            try
            {
                var mailmsg = new MailMessage
                {
                    IsBodyHtml = false,
                    From = new MailAddress(MailSettingModel.Instance.FromAddress, MailSettingModel.Instance.FromDisplayName),
                    Subject = $"{model.Token} là mã xác thực tài khoản RentEZ của bạn"
                };
                mailmsg.To.Add(model.Email);

                mailmsg.Body = $"Mã OTP của bạn là: {model.Token} " +
                               $"\nVui lòng nhập mã này để xác minh và hoàn tất việc tạo tài khoản.";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = EMAIL_SENDER_HOST;

                smtp.Port = EMAIL_SENDER_PORT;

                smtp.EnableSsl = EMAIL_IsSSL;

                var network = new NetworkCredential(EMAIL_SENDER, EMAIL_SENDER_PASSWORD);
                smtp.Credentials = network;

                smtp.Send(mailmsg);
            }
            catch (Exception ex)
            {
                throw new AppException(ErrorCode.Unknown, ex.Message);
            }

        }

        private void CreateResetPassMail(SendMailModel model)
        {
            try
            {
                var mailmsg = new MailMessage
                {
                    IsBodyHtml = false,
                    From = new MailAddress(MailSettingModel.Instance.FromAddress, MailSettingModel.Instance.FromDisplayName),
                    Subject = ""
                };
                mailmsg.To.Add(model.Email);

                mailmsg.Body = $"OTP reset: {model.Token}";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = EMAIL_SENDER_HOST;

                smtp.Port = EMAIL_SENDER_PORT;

                smtp.EnableSsl = EMAIL_IsSSL;
                var network = new NetworkCredential(EMAIL_SENDER, EMAIL_SENDER_PASSWORD);
                smtp.Credentials = network;
                smtp.Send(mailmsg);
            }
            catch (Exception ex)
            {
                throw new AppException(ErrorCode.Unknown, ex.Message);
            }

        }
    }
}
﻿
using System.Net.Mail;
using System.Net;
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Service.IService;

namespace LaBenVi_AuthService.Service
{
    public class MessengerService : IMessengerService
    {
        private readonly Dictionary<string, string> _config;

        public MessengerService(IConfiguration config)
        {
            _config = config.GetRequiredSection("EmailSettings").GetChildren()
                .ToDictionary(child => child.Key, child => child.Value);
        }

        public async Task<bool> Send(EmailLogger message, string attachment = "")
        {
            string GmailAccount = _config["SenderEmail"];
            string GmailPassword = _config["AppPassword"];
            IList<string> ToEmails = message.To;

            MailMessage appMail = new();

            foreach (string toEmail in ToEmails)
            {
                appMail.To.Add(toEmail);
            }

            appMail.From = new MailAddress(GmailAccount);
            appMail.Sender = new MailAddress(GmailAccount);
            appMail.Subject = message.Subject;
            appMail.Body = message.Body;
            appMail.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(attachment))
            {
                Attachment attach = new(attachment);
                appMail.Attachments.Add(attach);
                appMail.Priority = MailPriority.High;
            }

            System.Net.Mail.SmtpClient smtpClient = new(_config["Host"], int.Parse(_config["Port"]));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(GmailAccount, GmailPassword);
            smtpClient.Send(appMail);

            return true;
        }
    }
}

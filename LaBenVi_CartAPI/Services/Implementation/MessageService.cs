using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace LaBenVi_CartAPI.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;

        public MessageService(IConfiguration config)
        {
            _senderEmail = config["EmailSettings:SenderEmail"];

            // Initialize SmtpClient with SMTP server settings from configuration
            _smtpClient = new SmtpClient
            {
                Host = config["EmailSettings:Host"],
                Port = int.Parse(config["EmailSettings:Port"]),
                EnableSsl = bool.Parse(config["EmailSettings:EnableSsl"]),
                Credentials = new NetworkCredential(
                    config["EmailSettings:Username"],
                    config["EmailSettings:Password"])
            };
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            try
            {
                await _smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                return false;
            }
            finally
            {
                message.Dispose(); // Dispose of the MailMessage object
            }
        }
    }

}

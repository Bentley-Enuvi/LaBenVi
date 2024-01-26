//using Azure.Messaging.ServiceBus;
//using MessageRoute.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Microsoft.Extensions.Configuration;

//namespace MessageRoute
//{
//    public class MessageService : IMessageService
//    {
//        private readonly Dictionary<string, string> _config;
//        private string connectionString = "Endpoint=sb://mangoweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=HjoslS58pPHtAULb0tay/jx4Ys0+MO5/R+ASbCcFTG0=";

//        public MessageService(IConfiguration config)
//        {
//            _config = config.GetRequiredSection("EmailSettings").GetChildren()
//                .ToDictionary(child => child.Key, child => child.Value);
//        }

//        public async Task PublishMessage(object message, string topic_queue_Name)
//        {
//            await using var client = new ServiceBusClient(connectionString);

//            ServiceBusSender sender = client.CreateSender(topic_queue_Name);

//            var jsonMessage = JsonConvert.SerializeObject(message);
//            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding
//                .UTF8.GetBytes(jsonMessage))
//            {
//                CorrelationId = Guid.NewGuid().ToString(),
//            };

//            await sender.SendMessageAsync(finalMessage);
//            await client.DisposeAsync();
//        }



//        public async Task<bool> Send(Message message, string attachment = "")
//        {
//            string senderEmail = _config["SenderEmail"];
//            string appPassword = _config["AppPassword"];

//            using var appMail = new MailMessage
//            {
//                From = new MailAddress(senderEmail),
//                Sender = new MailAddress(senderEmail),
//                Subject = message.Subject,
//                Body = message.Body,
//                IsBodyHtml = true
//            };

//            foreach (var toEmail in message.To)
//            {
//                appMail.To.Add(toEmail);
//            }

//            if (!string.IsNullOrEmpty(attachment))
//            {
//                var attach = new Attachment(attachment);
//                appMail.Attachments.Add(attach);
//                appMail.Priority = MailPriority.High;
//            }

//            using var smtpClient = new SmtpClient(_config["Host"], int.Parse(_config["Port"]))
//            {
//                EnableSsl = true,
//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential(senderEmail, appPassword)
//            };

//            await Task.Run(() => smtpClient.Send(appMail));
//            return true;
//        }
//    }

//}

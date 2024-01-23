using MessageRoute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageRoute
{
    public interface IMessageService
    {
        //Task PublishMessage(object  message, string topic_queue_Name);
        Task<bool> Send(Message message, string attachment = "");
    }
}

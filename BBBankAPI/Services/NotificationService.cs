using Azure.Messaging.ServiceBus;
using Entites;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly ServiceBusSender _sender;
        public NotificationService(string connectionString, string topicName)
        {
            var client = new ServiceBusClient(connectionString);
            _sender = client.CreateSender(topicName);
        }
        public async Task SendEmailNotification(EmailNotificationDto notification)
        {
            string messageBody = JsonSerializer.Serialize(notification);
            ServiceBusMessage message = new ServiceBusMessage(messageBody);
            await _sender.SendMessageAsync(message);
        }
    }
}

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BBBankFunctions
{
    public class EmailProcessor
    {
        private readonly ILogger<EmailProcessor> _logger;
        private readonly string _communicationServicesConnectionString;
        public EmailProcessor(ILogger<EmailProcessor> logger, IConfiguration configuration)
        {
            _logger = logger;
            _communicationServicesConnectionString = configuration["BBBankAzureCommServicesConnString"];
        }

        [Function(nameof(EmailProcessor))]
        public async Task Run(
            [ServiceBusTrigger("email-notifications", "email-subscription", Connection = "BBBankServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            EmailNotificationDto notification = JsonSerializer.Deserialize<EmailNotificationDto>(message.Body);

            EmailClient emailClient = new EmailClient(_communicationServicesConnectionString);

            EmailSendOperation sendOperation = await emailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    notification.FromEmail,
                    notification.ToEmail,
                    notification.Subject,
                    notification.Body);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}

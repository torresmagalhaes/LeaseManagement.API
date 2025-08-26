using LeaseManagement.Domain.Entities;
using LeaseManagement.Domain.MongoDB.Entities;
using LeaseManagement.Infrastructure.MongoDB.Implementation;
using LeaseManagement.Infrastructure.RabbitMQ.Consume;
using LeaseManagementAPI.Mappers;
using System.Text.Json;

namespace RentManagementAPI.Services
{
    public class NotificationConsumerService : IHostedService
    {
        private readonly NotificationMessageConsumer _consumer;
        private readonly NotificationImplementation _notificationImplementation;

        public NotificationConsumerService(NotificationImplementation notificationImplementation)
        {
            _notificationImplementation = notificationImplementation;
            _consumer = new NotificationMessageConsumer(HandleMessage);
        }

        private void HandleMessage(string message)
        {
            var notificationDocument = JsonSerializer.Deserialize<NotificationDocument>(message);
            _notificationImplementation.InsertNotification(notificationDocument);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.StartConsuming();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
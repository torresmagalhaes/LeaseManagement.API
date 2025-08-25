using LeaseManagement.Domain.Entities;
using LeaseManagement.Infrastructure.MongoDB.Implementation;
using LeaseManagement.Infrastructure.RabbitMQ.Consume;
using LeaseManagementAPI.Mappers;
using System.Text.Json;

namespace RentManagementAPI.Services
{
    public class MotorcycleConsumerService : IHostedService
    {
        private readonly MotorcycleMessageConsumer _consumer;
        private readonly MotorcycleImplementation _motorcycleImplementation;

        public MotorcycleConsumerService(MotorcycleImplementation motorcycleImplementation)
        {
            _motorcycleImplementation = motorcycleImplementation;
            _consumer = new MotorcycleMessageConsumer(HandleMessage);
        }

        private void HandleMessage(string message)
        {
            var motorcycle = JsonSerializer.Deserialize<Motorcycle>(message);
            var motorcycleDocument = MotorcycleMapper.JsonToDocumentMapper(motorcycle);
            _motorcycleImplementation.InsertMotorcycle(motorcycleDocument);
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
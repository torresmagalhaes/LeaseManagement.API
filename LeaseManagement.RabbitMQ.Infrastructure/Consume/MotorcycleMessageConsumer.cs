using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LeaseManagement.Infrastructure.RabbitMQ.Consume
{
    public class MotorcycleMessageConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly Action<string> _messageHandler;

        public MotorcycleMessageConsumer(Action<string> messageHandler)
        {
            _messageHandler = messageHandler;
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "motorcycle",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    _messageHandler(message);
                    
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(
                queue: "motorcycle",
                autoAck: false,
                consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
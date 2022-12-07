using RabbitMQ.Client;
using System;

namespace ReportService.Clients.MessageBus
{
    public class MessageBusClient
    {
        #region Fields

        private readonly IConnection _connection;
        private readonly IModel _channel;

        #endregion Fields

        #region Ctor

        public MessageBusClient()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "admin",
                Password = "admin"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("reports",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        }

        #endregion Ctor

        #region Methods

        public void Send(int reportId)
        {
            _channel.BasicPublish("", "reports", null, BitConverter.GetBytes(reportId));
        }

        #endregion Methods
    }
}
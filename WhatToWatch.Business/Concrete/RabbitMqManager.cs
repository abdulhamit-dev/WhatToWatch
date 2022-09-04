using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.Business.Concrete
{
    public class RabbitMqManager : IRabbitMqService
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "MailExchange";
        public static string RoutingMail = "mail-route-send";
        public static string QueueName = "queue-mail-send";

        public RabbitMqManager(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingMail);
            return _channel;
        }

        public void Publish(MovieMailCreatedEvent movieMailCreatedEvent)
        {
            var channel = Connect();

            var bodyString =JsonConvert.SerializeObject(movieMailCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange:ExchangeName, routingKey: RoutingMail, basicProperties: properties, body: bodyByte);
        }
    }
}

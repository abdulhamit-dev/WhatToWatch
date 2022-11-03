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
            //kanal oluşturuldu
            _channel = _connection.CreateModel();

            //exchange mesajları ilgili kuyruklara rootlama yapar

            //fanout kendisene bağlı olan tüm kuyruklara mesaj iletir. aynı mesajı hepsine gönderir
            //hava tahmini örnek verilebilir. Bir tane hava tahmini yapılır saat başı veri gönderilir. İsteyenler queue oluşturarak bu veriyi alabilir.

            //direct root adına göre kuyruğa veri gönderilir
            //loglama örnek verilebilir. Error ,İnfo,Warning

            //topic exchange detaylı rootlama yapmakiçin
            //örnek olarak string ifadeler noktalar ile birbirinden ayrılarak daha detaylı varyasyon yapılır.

            //header exhange 
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            //kuyruk oluşturma
            //QueueName : kuyruk ismi
            //durable:true olursa kuyruk kaydedilir silinmez
            //exclusive:false ise farklı kanallar ile bağlanılabilir
            //autoDelete:false olursa bu durumda son subscriber da düşse bile que silinmez
            _channel.QueueDeclare(QueueName,durable: true,exclusive: false, false, null);

            //mesajı gönderme
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

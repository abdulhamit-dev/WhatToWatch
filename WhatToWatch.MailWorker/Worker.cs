using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.MailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IMailService _mailService;
        private readonly IMovieService _movieService;
        private IModel _channel;
        public Worker(ILogger<Worker> logger, IRabbitMqService rabbitMqService, IMailService mailService, IMovieService movieService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _mailService = mailService;
            _movieService = movieService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqService.Connect();

            //mesajlar kaçar kaçar gelsin
            //0 : herhangi bir boyutdaki mesaj gelebilir.
            //1 : 1 'er mesaj gelsin
            //global: false olursa her kullanýcýya 1 tane gitmesi true olursa  toplamý eþit bir þekilde böleerek daðýtma yapacak
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            //autoAck false ise otomatik olarak silmemesi için
            _channel.BasicConsume(RabbitMqManager.QueueName, false, consumer);
            consumer.Received += Consumer_Received;

            await Task.CompletedTask;

        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var movieMailCreatedEvent = JsonConvert.DeserializeObject<MovieMailCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            var movieDto = _movieService.GetById(movieMailCreatedEvent.MovieId);

            _mailService.SendMail(movieMailCreatedEvent.Mail, movieMailCreatedEvent.UserName, movieDto.Data);

            _channel.BasicAck(@event.DeliveryTag, false);

            return Task.CompletedTask;
        }
    }
}
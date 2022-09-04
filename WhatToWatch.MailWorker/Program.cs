using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
using WhatToWatch.Core.Caching;
using WhatToWatch.Core.Caching.Redis;
using WhatToWatch.Core.Entities;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Repositories;
using WhatToWatch.MailWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        services.AddSingleton(sp => new ConnectionFactory()
        { Uri = new Uri(hostContext.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IRabbitMqService, RabbitMqManager>();
        services.AddSingleton<IMailService, MailManager>();
        services.AddSingleton<IMovieService, MovieManager>();
        services.AddSingleton<IMovieDal, EfMovieDal>();
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton<RedisServer>();
        services.AddSingleton<RabbitMqManager>();
        services.AddHostedService<Worker>();

        var emailConfig = hostContext.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);
    })
    .Build();

await host.RunAsync();

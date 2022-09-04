using Microsoft.AspNetCore.Http;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
using WhatToWatch.Core.Caching.Redis;
using WhatToWatch.Core.Caching;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Repositories;
using WhatToWatch.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpClient();
        services.AddSingleton<IMovieService, MovieManager>();
        services.AddSingleton<IMovieDal, EfMovieDal>();
        services.AddHostedService<Worker>();
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton<RedisServer>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    })
    .Build();

await host.RunAsync();

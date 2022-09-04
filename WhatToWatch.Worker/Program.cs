using Microsoft.AspNetCore.Http;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Concrete;
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
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    })
    .Build();

await host.RunAsync();

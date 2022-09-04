using Newtonsoft.Json;
using WhatToWatch.Business.Abstract;

namespace WhatToWatch.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMovieService _movieService;

        public Worker(ILogger<Worker> logger,IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //quartz.net de kullanýlabilir di fakat tek job olduðu için gerek duymadým.
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var data = await _movieService.SaveData();
                    _logger.LogInformation("Worker SaveData() : {time}", DateTimeOffset.Now);
                    _logger.LogInformation("Worker Data() : ", JsonConvert.SerializeObject(data));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Worker Error", JsonConvert.SerializeObject(ex));
                }
                
                
                await Task.Delay(1000*60, stoppingToken);//1000*60*60=1hour 
            }
        }
    }
}
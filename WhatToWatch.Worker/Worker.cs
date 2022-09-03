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
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var data = await _movieService.SaveData();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    string err = ex.ToString();
                }
                
                
                await Task.Delay(1000*60, stoppingToken);//1000*60*60=1hour //
            }
        }
    }
}
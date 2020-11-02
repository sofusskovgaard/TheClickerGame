using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheClickerGame.Services.Services.CounterService;

namespace TheClickerGame.Services.HostedServices.CounterHostedService
{
    public class CounterHostedService : BackgroundService, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<CounterHostedService> _logger;

        public CounterHostedService(ILogger<CounterHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();

                    var counterService = scope.ServiceProvider.GetRequiredService<ICounterService>();

                    await counterService.IncrementCounterAsync();
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message, ex);
                }
                finally
                {
                    await Task.Delay(500, stoppingToken);
                }
            }
        }
    }
}

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using TheClickerGame.Services.Services.CounterService;

namespace TheClickerGame.Services.HostedServices.CounterHostedService
{
    public class CounterHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        public IServiceProvider Services { get; }

        public CounterHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using var scope = Services.CreateScope();

            var counterService = scope.ServiceProvider.GetRequiredService<ICounterService>();

            counterService.IncrementCounter();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

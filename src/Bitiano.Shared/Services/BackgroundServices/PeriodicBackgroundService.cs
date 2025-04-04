using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bitiano.Shared.Services.BackgroundServices;

public abstract class PeriodicBackgroundService : BackgroundService
{
    private readonly ILogger<PeriodicBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _serviceName;

    protected abstract TimeSpan Interval { get; }

    protected PeriodicBackgroundService(ILogger<PeriodicBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _serviceName = GetType().Name;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                await ExecuteJobAsync(scope, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "exception thrown at periodic background service {service}.", _serviceName);
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }

    protected abstract Task ExecuteJobAsync(IServiceScope scope, CancellationToken cancellationToken);
}
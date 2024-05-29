using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShelfApi.Application.BaseDataApplication.Interfaces;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataHostedService(IServiceProvider serviceProvider, IBaseDataService baseDataService) : IHostedLifecycleService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IBaseDataService _baseDataService = baseDataService;

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        IShelfApiDbContext shelfApiDbContext = scope.ServiceProvider.GetRequiredService<IShelfApiDbContext>();
        await _baseDataService.InitializeAsync(shelfApiDbContext);
    }

    public Task StartAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StartedAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StoppingAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StoppedAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
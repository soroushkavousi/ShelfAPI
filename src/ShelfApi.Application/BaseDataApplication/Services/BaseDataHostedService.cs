using Microsoft.Extensions.Hosting;
using ShelfApi.Application.BaseDataApplication.Interfaces;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataHostedService(IBaseDataService baseDataService) : IHostedLifecycleService
{
    private readonly IBaseDataService _baseDataService = baseDataService;

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        await _baseDataService.InitializeAsync();
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
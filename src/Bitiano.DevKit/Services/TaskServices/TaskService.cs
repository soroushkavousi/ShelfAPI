using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bitiano.DevKit.Services.TaskServices;

public class TaskService : ITaskService
{
    private readonly IServiceProvider _serviceProvider;

    public TaskService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<T> RunInANewThread<T>(IRequest<T> request)
    {
        return RunInANewThread(async (scope) =>
        {
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return await mediator.Send(request);
        });
    }

    public Task RunInANewThread(IRequest request)
    {
        return RunInANewThread(async (scope) =>
        {
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(request);
        });
    }

    public Task RunInANewThread(Func<IServiceScope, Task> function)
    {
        return Task.Run(async () =>
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            await function(scope);
        });
    }

    public Task<T> RunInANewThread<T>(Func<IServiceScope, Task<T>> function)
    {
        return Task.Run(async () =>
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            return await function(scope);
        });
    }

    public void FireAndForget<T>(IRequest<T> command)
    {
        _ = RunInANewThread(command);
    }

    public void FireAndForget(IRequest command)
    {
        _ = RunInANewThread(command);
    }

    public void FireAndForget(Func<IServiceScope, Task> function)
    {
        _ = RunInANewThread(function);
    }

    public void FireAndForget<T>(Func<IServiceScope, Task<T>> function)
    {
        _ = RunInANewThread(function);
    }
}
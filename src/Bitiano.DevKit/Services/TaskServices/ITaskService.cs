using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bitiano.DevKit.Services.TaskServices;

public interface ITaskService
{
    Task<T> RunInANewThread<T>(IRequest<T> request);

    Task RunInANewThread(IRequest request);

    Task RunInANewThread(Func<IServiceScope, Task> function);

    Task<T> RunInANewThread<T>(Func<IServiceScope, Task<T>> function);

    void FireAndForget<T>(IRequest<T> command);

    void FireAndForget(IRequest command);

    void FireAndForget(Func<IServiceScope, Task> function);

    void FireAndForget<T>(Func<IServiceScope, Task<T>> function);
}
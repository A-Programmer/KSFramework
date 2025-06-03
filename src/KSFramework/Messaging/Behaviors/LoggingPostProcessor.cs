using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Behaviors;

public class LoggingPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
{
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[PostProcessor] Handled request of type: {typeof(TRequest).Name} with response: {response}");
        return Task.CompletedTask;
    }
}
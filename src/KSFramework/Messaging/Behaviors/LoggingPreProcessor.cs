using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Behaviors;

public class LoggingPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[PreProcessor] Handling request of type: {typeof(TRequest).Name}");
        return Task.CompletedTask;
    }
}
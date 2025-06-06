using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;
namespace KSFramework.UnitTests.Stream.CreateStream;

/// <summary>
/// Unit tests for Mediator's CreateStream method and stream pipeline behaviors.
/// </summary>
public class MediatorCreateStreamTests
{
    /// <summary>
    /// A sample stream request that streams integers up to Count.
    /// </summary>
    public class StreamRequest : IStreamRequest<int>
    {
        public int Count { get; set; }
    }

    /// <summary>
    /// A handler for <see cref="StreamRequest"/> that streams integers from 1 to Count.
    /// </summary>
    public class StreamHandler : IStreamRequestHandler<StreamRequest, int>
    {
        public async IAsyncEnumerable<int> Handle(StreamRequest request, CancellationToken cancellationToken)
        {
            for (int i = 1; i <= request.Count; i++)
            {
                yield return i;
                await Task.Delay(10, cancellationToken); // simulate async work
            }
        }
    }

    /// <summary>
    /// A simple logging behavior for streams that logs before and after streaming.
    /// </summary>
    public class LoggingBehavior : IStreamPipelineBehavior<StreamRequest, int>
    {
        public List<string> Logs { get; } = new();

        public async IAsyncEnumerable<int> Handle(
            StreamRequest request,
            CancellationToken cancellationToken,
            StreamHandlerDelegate<int> next)
        {
            Logs.Add("[Before] Handling StreamRequest");

            await foreach (var item in next())
            {
                yield return item;
            }

            Logs.Add("[After] Handling StreamRequest");
        }
    }

    [Fact]
    public async Task CreateStream_Should_InvokeHandler_AndBehavior()
    {
        var services = new ServiceCollection();

        var behavior = new LoggingBehavior();

        services.AddSingleton<IMediator, KSFramework.KSMessaging.Mediator>();
        services.AddScoped<IStreamRequestHandler<StreamRequest, int>, StreamHandler>();
        services.AddScoped<IStreamPipelineBehavior<StreamRequest, int>>(_ => behavior);

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        var request = new StreamRequest { Count = 3 };
        var results = new List<int>();

        await foreach (var item in mediator.CreateStream(request))
        {
            results.Add(item);
        }

        Assert.Equal(new[] { 1, 2, 3 }, results);
        Assert.Equal(2, behavior.Logs.Count);
        Assert.Contains(behavior.Logs, x => x.Contains("[Before]"));
        Assert.Contains(behavior.Logs, x => x.Contains("[After]"));
    }
}
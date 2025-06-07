using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Samples;

public class CounterStreamRequest : IStreamRequest<int>
{
    public int CountTo { get; }

    public CounterStreamRequest(int countTo)
    {
        CountTo = countTo;
    }
}

public class CounterStreamHandler : IStreamRequestHandler<CounterStreamRequest, int>
{
    public async IAsyncEnumerable<int> Handle(CounterStreamRequest request, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (int i = 1; i <= request.CountTo; i++)
        {
            yield return i;
            await Task.Delay(200, cancellationToken); // simulate delay
        }
    }
}
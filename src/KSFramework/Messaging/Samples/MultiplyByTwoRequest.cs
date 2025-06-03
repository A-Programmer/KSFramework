using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Samples;

public class MultiplyByTwoRequest : IRequest<int>
{
    public int Input { get; }

    public MultiplyByTwoRequest(int input) => Input = input;
}

public class MultiplyByTwoHandler : IRequestHandler<MultiplyByTwoRequest, int>
{
    public Task<int> Handle(MultiplyByTwoRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.Input * 2);
    }
}
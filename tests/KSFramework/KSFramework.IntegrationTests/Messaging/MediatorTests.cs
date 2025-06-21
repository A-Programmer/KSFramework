using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.IntegrationTests.Messaging;

public record TestCommand : ICommand<string>
{
    public string Data { get; init; } = string.Empty;
}

public class TestCommandHandler : ICommandHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand command, CancellationToken cancellationToken = default)
    {
        return Task.FromResult($"Handled: {command.Data}");
    }
}

public record TestQuery : IQuery<string>
{
    public string Data { get; init; } = string.Empty;
}

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public Task<string> Handle(TestQuery query, CancellationToken cancellationToken = default)
    {
        return Task.FromResult($"Queried: {query.Data}");
    }
}

public class MediatorTests : IntegrationTestBase
{
    public MediatorTests()
    {
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddKSFramework(typeof(MediatorTests).Assembly);
    }

    [Fact]
    public async Task SendCommand_ShouldExecuteCommandHandler()
    {
        // Arrange
        using var scope = CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new TestCommand { Data = "Test Command" };

        // Act
        var result = await mediator.Send(command);

        // Assert
        Assert.Equal("Handled: Test Command", result);
    }

    [Fact]
    public async Task SendQuery_ShouldExecuteQueryHandler()
    {
        // Arrange
        using var scope = CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new TestQuery { Data = "Test Query" };

        // Act
        var result = await mediator.Send(query);

        // Assert
        Assert.Equal("Queried: Test Query", result);
    }
}
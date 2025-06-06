# KSFramework 🧩

**KSFramework** is a clean, extensible, and testable foundation for building scalable .NET Core applications. It provides a custom implementation of the MediatR pattern, along with well-known enterprise patterns such as Repository, Unit of Work, and Specification. It is designed to be modular, testable, and ready for production use.

---

## ✨ Features

- ✅ Custom Mediator pattern implementation (Send / Publish / Stream)
- ✅ Pipeline behaviors (Validation, Logging, Exception handling, Pre/Post-processors)
- ✅ FluentValidation integration
- ✅ Notification pipeline behaviors
- ✅ Repository Pattern
- ✅ Unit of Work Pattern
- ✅ Specification Pattern
- ✅ Scrutor-based automatic registration
- ✅ File-scoped namespaces and XML documentation for every component
- ✅ Full unit test coverage using xUnit and Moq
- ✅ Swagger/OpenAPI documentation support
- ✅ Comprehensive XML documentation

---

## 📦 Installation

Add the package reference (once published):

```bash
dotnet add package KSFramework.KSMessaging
dotnet add package KSFramework.KSData
```

Or reference the source projects directly in your solution.

## 📚 API Documentation

### Swagger/OpenAPI Setup

1. Add Swagger configuration in your `Program.cs`:
```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Your API Name",
        Version = "v1",
        Description = "API Documentation"
    });
    
    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
```

2. Enable Swagger UI in your application:
```csharp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});
```

### XML Documentation

The framework is configured to generate XML documentation. Add XML comments to your classes and methods:

```csharp
/// <summary>
/// Represents a generic repository for entity operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity> GetByIdAsync(object id);
}
```
⸻

🧠 Project Structure  
```
src/
KSFramework.KSMessaging/           → Custom mediator, behaviors, contracts
KSFramework.KSData/                → Repository, UnitOfWork, Specification
KSFramework.KSSharedKernel/        → Domain base types, entities, value objects

tests/
KSFramework.UnitTests/           → xUnit unit tests

samples/
MediatorSampleApp/               → Console app to demonstrate usage
```  

### 🚀 Mediator Usage

### 1. Define a request  
```csharp
public class MultiplyByTwoRequest : IRequest<int>
{
    public int Input { get; }
    public MultiplyByTwoRequest(int input) => Input = input;
}
```  

### 2. Create a handler  
```csharp
public class MultiplyByTwoHandler : IRequestHandler<MultiplyByTwoRequest, int>
{
    public Task<int> Handle(MultiplyByTwoRequest request, CancellationToken cancellationToken)
        => Task.FromResult(request.Input * 2);
}
```  

### 3. Send the request  
```csharp
var result = await mediator.Send(new MultiplyByTwoRequest(5));
Console.WriteLine(result); // Output: 10
```  

### 📤 Notifications

### Define a notification and handler  
```csharp
public class UserRegisteredNotification : INotification
{
    public string Username { get; }
    public UserRegisteredNotification(string username) => Username = username;
}

public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredNotification>
{
    public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Welcome email sent to {notification.Username}");
        return Task.CompletedTask;
    }
}
```  

### Publish the notification  
```csharp
await mediator.Publish(new UserRegisteredNotification("john"));
```  

### 🔁 Streaming

### Define a stream request and handler  
```csharp
public class CounterStreamRequest : IStreamRequest<int>
{
    public int Count { get; init; }
}

public class CounterStreamHandler : IStreamRequestHandler<CounterStreamRequest, int>
{
    public async IAsyncEnumerable<int> Handle(CounterStreamRequest request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        for (int i = 1; i <= request.Count; i++)
        {
            yield return i;
            await Task.Delay(10, cancellationToken);
        }
    }
}
```  

### Consume the stream  
```csharp
await foreach (var number in mediator.CreateStream(new CounterStreamRequest { Count = 3 }))
{
    Console.WriteLine($"Streamed: {number}");
}
```  

## 🧩 Built-in Pipeline Behaviors

### All behaviors are automatically registered via AddKSMediator().  
```
| Behavior                   | Description                                     |
|---------------------------|-------------------------------------------------|
| RequestValidationBehavior | Validates incoming requests using FluentValidation |
| ExceptionHandlingBehavior | Logs and rethrows exceptions from handlers     |
| RequestProcessorBehavior  | Executes pre- and post-processors              |
| LoggingBehavior           | Logs request and response types                |
| NotificationLoggingBehavior | Logs notification handling stages            |
```  

## 🧰 Configuration

## Register services in Program.cs  
```csharp
services.AddLogging();
services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddKSMediator(Assembly.GetExecutingAssembly());
```  

## 🧪 Unit Testing

### Example behavior test  
```csharp
[Fact]
public async Task Handle_WithInvalidRequest_ThrowsValidationException()
{
    var validator = new Mock<IValidator<TestRequest>>();
    validator.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Name", "Required") }));

    var logger = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

    var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
        new[] { validator.Object }, logger.Object);

    await Assert.ThrowsAsync<ValidationException>(() =>
        behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse())));
}
```  

## 📦 Repository & Unit of Work  
```csharp
public class ProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(Product product)
    {
        await _repository.AddAsync(product);
        await _unitOfWork.CommitAsync();
    }
}
```  

## 🔍 Specification Pattern  
```csharp
public class ActiveProductSpec : Specification<Product>
{
    public ActiveProductSpec() => Criteria = p => p.IsActive;
}
```  

```csharp
var products = await _repository.ListAsync(new ActiveProductSpec());
```  

## ✅ Test Coverage Summary

```
| Component               | Test Status |
|------------------------|-------------|
| Request handling       | ✅          |
| Notification publishing| ✅          |
| Streaming requests     | ✅          |
| Pipeline behaviors     | ✅          |
| Validation             | ✅          |
| Exception handling     | ✅          |
| Logging                | ✅          |
| Repository/UoW/Spec    | ✅          |
```  

## 📚 License

### This project is licensed under the MIT License.

## 👥 Contributing

### Feel free to fork and submit PRs or issues. Contributions are always welcome!
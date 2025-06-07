# KSFramework

**KSFramework** is a modular and extensible .NET framework that simplifies the implementation of enterprise-grade applications using **Clean Architecture**, **DDD**, and **CQRS** patterns. It includes built-in support for Generic Repository, Unit of Work, and a custom MediatR-style messaging system.

---

## ✨ Features

- ✅ Clean Architecture and DDD-friendly structure
- ✅ Generic Repository with constraint on AggregateRoots
- ✅ Fully extensible Unit of Work pattern
- ✅ Built-in Pagination utilities
- ✅ Internal MediatR-like message dispatch system (KSMessaging)
- ✅ Scrutor-based handler and behavior registration
- ✅ Support for pipeline behaviors (logging, validation, etc.)
- ✅ Strongly testable, loosely coupled abstractions

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package KSFramework
```

---

## 🧰 Modules Overview

- `KSFramework.KSDomain` — Domain primitives: `Entity`, `AggregateRoot`, `ValueObject`
- `KSFramework.GenericRepository` — `Repository`, `UnitOfWork`, pagination support
- `KSFramework.KSMessaging` — CQRS with internal MediatR-style handler resolver, behaviors, stream handling

---

## ⚙️ Usage Overview

### 🧱 Register Services (Program.cs)

```csharp
builder.Services.AddKSMediator(typeof(Program).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

### 🧪 Sample Command

```csharp
public record CreatePostCommand(string Title, string Content) : ICommand<Guid>;

public class CreatePostHandler : ICommandHandler<CreatePostCommand, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreatePostHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new BlogPost { Id = Guid.NewGuid(), Title = request.Title, Content = request.Content };
        await _uow.GetRepository<BlogPost>().AddAsync(post);
        await _uow.SaveChangesAsync();
        return post.Id;
    }
}
```

---

## 📚 Example: Blog with Newsletter

### ✍️ Features

- CRUD for Blog Posts using Commands/Queries
- Subscriber registration using Notification pattern
- Weekly newsletter dispatch via background service

---

### 🧩 Domain Layer

```csharp
public class BlogPost : AggregateRoot<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedAt { get; set; }
}

public class Subscriber : Entity<Guid>
{
    public string Email { get; set; }
}
```

---

### 🧠 Application Commands

```csharp
public record SubscribeCommand(string Email) : ICommand;

public class SubscribeHandler : ICommandHandler<SubscribeCommand>
{
    private readonly IUnitOfWork _uow;

    public SubscribeHandler(IUnitOfWork uow) => _uow = uow;

    public async Task Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        var repo = _uow.GetRepository<Subscriber>();
        await repo.AddAsync(new Subscriber { Email = request.Email });
        await _uow.SaveChangesAsync();
    }
}
```

---

### 🌐 API Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly ISender _sender;

    public NewsletterController(ISender sender) => _sender = sender;

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromForm] string email)
    {
        await _sender.Send(new SubscribeCommand(email));
        return Ok("Subscribed");
    }
}
```

---

### 📨 Weekly Newsletter Job

```csharp
public class WeeklyNewsletterJob
{
    private readonly IUnitOfWork _uow;

    public WeeklyNewsletterJob(IUnitOfWork uow) => _uow = uow;

    public async Task SendAsync()
    {
        var posts = (await _uow.GetRepository<BlogPost>().GetAllAsync())
                    .Where(p => p.PublishedAt >= DateTime.UtcNow.AddDays(-7));

        var subscribers = await _uow.GetRepository<Subscriber>().GetAllAsync();

        foreach (var s in subscribers)
        {
            await EmailSender.Send(s.Email, "Your Weekly Digest", string.Join("

", posts.Select(p => p.Title)));
        }
    }
}
```

---

## 🧪 Testing

- Handlers and pipelines are fully testable using unit tests
- Use `KSFramework.UnitTests` project to validate handler behavior
- Custom behaviors can be tested independently

---

## 📌 Roadmap

- [x] Internal MediatR/CQRS support
- [x] Pagination
- [x] Unit of Work abstraction
- [ ] ValidationBehavior
- [ ] ExceptionHandlingBehavior
- [ ] Domain Events integration

---

## 📄 License

Licensed under MIT.
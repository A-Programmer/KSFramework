# KSFramework

**KSFramework** is a lightweight, extensible .NET framework designed to accelerate clean architecture and domain-driven development. It offers generic repository support, pagination utilities, and helpful abstractions to simplify the implementation of enterprise-grade backend systems using modern .NET patterns.

---

## ✨ Features

- 🧱 **Domain-Driven Design (DDD) Friendly**
- 🧼 **Clean Architecture Support**
- 🧰 **Generic Repository Pattern**
- 📄 **Built-in Pagination Support**
- 🧪 **Unit Testable Core Interfaces**
- 🧩 **Customizable and Extensible by Design**

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package KSFramework
```

Or via the Package Manager Console:

```powershell
Install-Package KSFramework
```

---

## 📂 Project Structure

The KSFramework package is modular and consists of:

- `KSFramework.GenericRepository` — Contains generic repository interfaces and base implementations.
- `KSFramework.Pagination` — Provides interfaces and classes for paginated queries.
- `KSFramework.KSDomain` — Base types for entities, aggregate roots, and value objects.

---

## 🚀 Getting Started

### 1. Define Your Entities

```csharp
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedAt { get; set; }
}
```

### 2. Setup Your DbContext

```csharp
public class AppDbContext : DbContext
{
    public DbSet<BlogPost> BlogPosts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
```

### 3. Create Your Repository

You can use the generic repository directly:

```csharp
public class BlogPostRepository : Repository<BlogPost>
{
    public BlogPostRepository(AppDbContext context) : base(context) { }
}
```

Or inject `IRepository<BlogPost>` if you're using dependency injection with Scrutor or your own DI container.

---

## 🧪 Common Repository Operations

```csharp
await _repository.AddAsync(new BlogPost { Title = "Welcome", Content = "This is the first post." });

var post = await _repository.GetByIdAsync(1);

await _repository.UpdateAsync(post);

await _repository.DeleteAsync(post);

var allPosts = await _repository.GetAllAsync();

var filtered = _repository.Find(p => p.PublishedAt > DateTime.UtcNow.AddDays(-30));
```

---

## 📘 Pagination Example

```csharp
var query = _repository.Query();
var paginated = await query.PaginateAsync(pageNumber: 1, pageSize: 10);
```

`PaginateAsync` is an extension method provided by the `KSFramework.Pagination` namespace.

---

## 📚 Full Example: Building a Blog with Weekly Newsletter

### Project Overview

This sample demonstrates how to build a simple blog platform using `KSFramework` with:

- Post publishing
- Subscriber registration
- Weekly newsletter delivery to subscribers

---

### Step 1: Define Entities

```csharp
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedAt { get; set; }
}

public class Subscriber
{
    public int Id { get; set; }
    public string Email { get; set; }
}
```

---

### Step 2: Repositories

```csharp
public class BlogPostRepository : Repository<BlogPost>
{
    public BlogPostRepository(AppDbContext context) : base(context) { }
}

public class SubscriberRepository : Repository<Subscriber>
{
    public SubscriberRepository(AppDbContext context) : base(context) { }
}
```

---

### Step 3: API Controllers

```csharp
[ApiController]
[Route("api/[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IRepository<BlogPost> _repository;

    public BlogPostsController(IRepository<BlogPost> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<BlogPost>> GetAll() => await _repository.GetAllAsync();

    [HttpPost]
    public async Task<IActionResult> Create(BlogPost post)
    {
        post.PublishedAt = DateTime.UtcNow;
        await _repository.AddAsync(post);
        return Ok(post);
    }
}
```

---

### Step 4: Subscription Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class NewsletterController : ControllerBase
{
    private readonly IRepository<Subscriber> _subRepo;

    public NewsletterController(IRepository<Subscriber> subRepo)
    {
        _subRepo = subRepo;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe(string email)
    {
        await _subRepo.AddAsync(new Subscriber { Email = email });
        return Ok("Subscribed");
    }
}
```

---

### Step 5: Weekly Newsletter Job (Example)

You can use [Hangfire](https://www.hangfire.io/) or any scheduler to run this task weekly:

```csharp
public class WeeklyNewsletterJob
{
    private readonly IRepository<Subscriber> _subRepo;
    private readonly IRepository<BlogPost> _postRepo;

    public WeeklyNewsletterJob(IRepository<Subscriber> subRepo, IRepository<BlogPost> postRepo)
    {
        _subRepo = subRepo;
        _postRepo = postRepo;
    }

    public async Task Send()
    {
        var lastWeek = DateTime.UtcNow.AddDays(-7);
        var posts = (await _postRepo.GetAllAsync())
                        .Where(p => p.PublishedAt > lastWeek)
                        .ToList();

        var subscribers = await _subRepo.GetAllAsync();

        foreach (var sub in subscribers)
        {
            // send email (via SMTP or 3rd party)
            await EmailSender.Send(sub.Email, "Weekly Newsletter", RenderPosts(posts));
        }
    }

    private string RenderPosts(IEnumerable<BlogPost> posts)
    {
        return string.Join("

", posts.Select(p => $"{p.Title}
{p.Content}"));
    }
}
```

---

## 📌 Roadmap

- [x] Generic Repository
- [x] Pagination
- [ ] Specification Pattern
- [ ] Auditing Support
- [ ] Integration with MediatR & CQRS

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repository
2. Create a feature branch
3. Submit a pull request

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 🔗 Related Projects

- [MediatR](https://github.com/jbogard/MediatR)
- [Scrutor](https://github.com/khellang/Scrutor)
- [Hangfire](https://www.hangfire.io/)
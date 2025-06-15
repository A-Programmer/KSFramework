# KSFramework

KSFramework یک فریمورک مدولار و قابل توسعه برای .NET است که پیاده‌سازی معماری تمیز (Clean Architecture)، DDD و CQRS را برای ساخت برنامه‌های سازمانی ساده می‌کند. این فریمورک شامل امکاناتی مانند ریپازیتوری جنریک، Unit of Work، سیستم پیام‌رسانی مشابه MediatR، متدهای کمکی، Enums و Exceptions رایج، API Result یکپارچه، ابزارهای صفحه‌بندی، پاسخ‌های عمومی، Tag Helperهای .NET Core و ابزارهای کاربردی دیگر است.

---

## ✨ ویژگی‌ها

- ساختار مناسب Clean Architecture و DDD
- ریپازیتوری جنریک با محدودیت روی AggregateRootها
- پیاده‌سازی کامل Unit of Work
- ابزارهای صفحه‌بندی داخلی
- سیستم پیام‌رسانی داخلی مشابه MediatR (KSMessaging)
- ثبت Handler و Behaviorها با Scrutor
- پشتیبانی از Pipeline Behaviorها (مانند لاگ‌گیری، اعتبارسنجی و ...)
- انتزاع‌های قابل تست و loosely coupled

---

## 📦 نصب

نصب از طریق NuGet:

```bash
dotnet add package KSFramework
```

---

## 🧰 ماژول‌ها

- `KSFramework.KSDomain` — موجودیت‌های پایه: `Entity`, `AggregateRoot`, `ValueObject`
- `KSFramework.GenericRepository` — ریپازیتوری، UnitOfWork، صفحه‌بندی
- `KSFramework.KSMessaging` — CQRS با Handler Resolver داخلی، Behaviorها، Stream Handling
- `KSFramework.Enums` — Enums رایج
- `KSFramework.Exceptions` — Exceptions رایج
- `KSFramework.KSApi` — API Result یکپارچه
- `KSFramework.Pagination` — ابزار صفحه‌بندی
- `KSFramework.Responses` — پاسخ‌های عمومی
- `KSFramework.TagHelpers` — Tag Helperهای .NET Core
- `KSFramework.Utilities` — متدهای کمکی

---

## ⚙️ نحوه استفاده

### ثبت سرویس‌ها (Program.cs)

```csharp
builder.Services.AddKSMediator(typeof(Program).Assembly); // اسمبلی‌هایی که Handlerها در آن قرار دارند
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

### نمونه Command

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

## 🧪 تست

- Handlerها و Pipelineها به راحتی قابل تست واحد هستند
- پروژه `KSFramework.UnitTests` برای تست رفتار Handlerها وجود دارد
- Behaviorهای سفارشی را می‌توانید به صورت مستقل تست کنید

---

## 📄 لایسنس

این پروژه تحت لایسنس MIT منتشر شده است.

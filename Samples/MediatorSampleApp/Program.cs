using FluentValidation;
using KSFramework.Messaging;
using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Behaviors;
using KSFramework.Messaging.Configuration;
using KSFramework.Messaging.Samples;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// فقط یکبار mediator رو ثبت کن
services.AddScoped<IMediator, Mediator>();
services.AddScoped<ISender>(sp => sp.GetRequiredService<IMediator>());

services.AddLogging();

services.AddValidatorsFromAssembly(typeof(Program).Assembly); 
services.AddMessaging(typeof(MultiplyByTwoHandler).Assembly);

// اگر رفتارهای pipeline داری، ثبت کن
services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestProcessorBehavior<,>));
services.AddMessaging(typeof(ExceptionHandlingBehavior<,>).Assembly, typeof(MultiplyByTwoRequest).Assembly);

var provider = services.BuildServiceProvider();

var mediator = provider.GetRequiredService<IMediator>();

var result = await mediator.Send(new MultiplyByTwoRequest(5));
Console.WriteLine($"Result: {result}");
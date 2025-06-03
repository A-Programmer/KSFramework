using System.Reflection;
using FluentValidation;
using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Extensions;
using KSFramework.Messaging.Samples;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddLogging();
services.AddValidatorsFromAssembly(typeof(Program).Assembly);


services.AddKSMediator(Assembly.GetExecutingAssembly());


var provider = services.BuildServiceProvider();

var mediator = provider.GetRequiredService<IMediator>();

var result = await mediator.Send(new MultiplyByTwoRequest(5));
Console.WriteLine($"Result: {result}");
using Microsoft.AspNetCore.HttpOverrides;
using Project.Application;
using Project.Domain;
using Project.Infrastructure;
using Project.Presentation;
using Project.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterWebApi();
builder.Services.RegisterApplication();
builder.Services.RegisterPresentation();
builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterDomain();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UsePresentation();
app.UseDomain();
app.UseApplication();
app.UseInfrastructure();
app.UseWebApi();


app.Run();

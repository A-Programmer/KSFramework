namespace Project.WebApi;

public static class DependencyInjection
{
    public static WebApplicationBuilder RegisterWebApi(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        return app;
    }
}
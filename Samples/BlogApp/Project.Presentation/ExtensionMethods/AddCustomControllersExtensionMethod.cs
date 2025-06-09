using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Presentation.ExtensionMethods;

public static class AddCustomControllersExtensionMethod
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(Application.AssemblyReference.Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        return services;
    }
}
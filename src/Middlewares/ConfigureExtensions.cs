using System;
using KSFramework.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.Middlewares
{
    public  static class ConfigureExtensions
    {
        public static IApplicationBuilder UseCustomExceptionLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggerMiddleware>();
        }

    }
}

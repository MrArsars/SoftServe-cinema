using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
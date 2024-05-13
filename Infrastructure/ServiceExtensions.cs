using BusinessLogic.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CinemaDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

    }
}

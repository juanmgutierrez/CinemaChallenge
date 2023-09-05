using Cinema.Application.Showtime.Repositories;
using Cinema.Infrastructure.Contexts;
using Cinema.Infrastructure.Respositories;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopmentEnvironment)
    {
        services.AddDbContexts(isDevelopmentEnvironment);
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, bool isDevelopmentEnvironment)
    {
        return services.AddDbContext<CinemaDbContext>(options =>
        {
            //options.UseSqlite("Data Source=cinema.db", b => b.MigrationsAssembly(typeof(CinemaDbContext).Assembly.FullName)));
            options.UseInMemoryDatabase("CinemaDb");

            if (isDevelopmentEnvironment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            }
        });
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IShowtimesRepository, ShowtimesRepository>();
        services.AddScoped<IAuditoriumsRepository, AuditoriumsRepository>();

        return services;
    }
}

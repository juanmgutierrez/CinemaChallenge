using Cinema.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure.InitializationExtensions;

public static class DatabaseInitialization
{
    public static WebApplication CreateDatabase(this WebApplication webApplication)
    {
        using var serviceScope = webApplication
            .Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var dbContext = serviceScope
            .ServiceProvider
            .GetService<CinemaDbContext>()!;

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        return webApplication;
    }

    public static WebApplication SeedDatabase(this WebApplication webApplication)
    {
        using var serviceScope = webApplication
            .Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var dbContext = serviceScope
            .ServiceProvider
            .GetService<CinemaDbContext>()!;

        SampleData.Initialize(dbContext);

        return webApplication;
    }
}

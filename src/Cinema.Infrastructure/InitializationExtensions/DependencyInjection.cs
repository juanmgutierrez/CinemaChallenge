using Cinema.Application.Auditorium.Repositories;
using Cinema.Application.Common.Proxies;
using Cinema.Application.Showtime.Repositories;
using Cinema.Infrastructure.Contexts;
using Cinema.Infrastructure.Proxies;
using Cinema.Infrastructure.Respositories;
using CinemaAPI.Database.Repositories.Abstractions;
using Grpc.Net.Client.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI;

namespace Cinema.Infrastructure.InitializationExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopmentEnvironment)
    {
        services.AddDbContexts(isDevelopmentEnvironment);
        services.AddRepositories();
        services.AddGrpcClients(isDevelopmentEnvironment);
        services.AddProxies();
        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, bool isDevelopmentEnvironment)
    {
        return services.AddDbContext<CinemaDbContext>(options =>
        {
            options.UseSqlite(
                // TODO Move ConnStr to appsettings.json
                @"Data Source=.\cinemaDB.db;",
                sqLiteOptions => sqLiteOptions.CommandTimeout(5));

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
        services.AddScoped<ISeatsRepository, SeatsRepository>();
        services.AddScoped<ITicketsRepository, TicketsRepository>();
        services.AddScoped<IMoviesRepository, MoviesRepository>();
        return services;
    }

    private static IServiceCollection AddGrpcClients(this IServiceCollection services, bool isDevelopmentEnvironment)
    {
        services.AddGrpcClient<MoviesApi.MoviesApiClient>(options =>
        {
            // TODO Extract to appsettings.json
            options.Address = new Uri("https://localhost:7443");
            // TODO Add interceptor for adding API Key

            options.ChannelOptionsActions.Add(channelOptions =>
            {
                if (isDevelopmentEnvironment)
                    channelOptions.HttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                channelOptions.ServiceConfig = new ServiceConfig
                {
                    MethodConfigs =
                    {
                        new MethodConfig
                        {
                            Names = { MethodName.Default },
                            RetryPolicy = new RetryPolicy
                            {
                                MaxAttempts = 5,
                                InitialBackoff = TimeSpan.FromSeconds(1),
                                MaxBackoff = TimeSpan.FromSeconds(5),
                                BackoffMultiplier = 2,
                                RetryableStatusCodes = { Grpc.Core.StatusCode.Unavailable }
                            }
                        }
                    }
                };

                // TODO Set timeouts
            });
        });

        return services;
    }

    private static IServiceCollection AddProxies(this IServiceCollection services)
    {
        services.AddScoped<IMoviesAPIProxy, MoviesAPIProxy>();
        return services;
    }
}

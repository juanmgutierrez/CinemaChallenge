using Cinema.Application.Common;
using Cinema.Application.Common.Behaviours;
using FluentValidation;
using MediatR;

namespace Cinema.API.Common;

public static class Extensions
{
    public static IServiceCollection AddCQRSBehavior(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

            // TODO Add Logging
            //config.AddBehavior<LoggingBehavior>();

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

        return services;
    }
}

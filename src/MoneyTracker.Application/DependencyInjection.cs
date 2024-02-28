using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker.Application.Abstractions.Behaviors;

namespace MoneyTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //services.AddTransient<CashingService>();

        return services;
    }
}
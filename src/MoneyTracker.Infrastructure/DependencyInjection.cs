using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker.Application.Abstractions.Clock;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Email;
using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.Repositories;
using MoneyTracker.Domain.Transactions.Repositories;
using MoneyTracker.Domain.Users.Repositories;
using MoneyTracker.Infrastructure.Clock;
using MoneyTracker.Infrastructure.Data;
using MoneyTracker.Infrastructure.Email;
using MoneyTracker.Infrastructure.Repositories;

namespace MoneyTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        string connectionString = configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}

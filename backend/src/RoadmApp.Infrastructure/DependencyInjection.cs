using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadmApp.Application.Abstractions;
using RoadmApp.Infrastructure.Persistence;
using RoadmApp.Infrastructure.Persistence.Repositories;
using RoadmApp.Infrastructure.Security;

namespace RoadmApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RoadmApp")
            ?? "Data Source=roadmapp.db";

        services.AddDbContext<RoadmAppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RoadmAppDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlannerRepository, PlannerRepository>();
        services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();

        return services;
    }
}

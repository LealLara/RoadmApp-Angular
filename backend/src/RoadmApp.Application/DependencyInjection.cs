using Microsoft.Extensions.DependencyInjection;
using RoadmApp.Application.Auth;
using RoadmApp.Application.Planning;

namespace RoadmApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<PlannerService>();

        return services;
    }
}

using RoadmApp.Application.Auth;

namespace RoadmApp.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Auth");

        group.MapPost("/register", async (
            RegisterUserRequest request,
            AuthService service,
            CancellationToken cancellationToken) =>
        {
            var response = await service.RegisterAsync(request, cancellationToken);
            return Results.Created($"/api/users/{response.User.Id}", response);
        });

        group.MapPost("/login", async (
            LoginRequest request,
            AuthService service,
            CancellationToken cancellationToken) =>
        {
            var response = await service.LoginAsync(request, cancellationToken);
            return Results.Ok(response);
        });

        return app;
    }
}

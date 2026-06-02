using RoadmApp.Application.Planning;

namespace RoadmApp.Api.Endpoints;

public static class PlannerEndpoints
{
    public static IEndpointRouteBuilder MapPlannerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users/{userId:guid}")
            .WithTags("Planner");

        group.MapGet("/dashboard", async (Guid userId, PlannerService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetDashboardAsync(userId, cancellationToken)));

        group.MapGet("/tasks", async (Guid userId, PlannerService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetTasksAsync(userId, cancellationToken)));

        group.MapPost("/tasks", async (
            Guid userId,
            UpsertTaskRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var task = await service.CreateTaskAsync(userId, request, cancellationToken);
            return Results.Created($"/api/users/{userId}/tasks/{task.Id}", task);
        });

        group.MapPut("/tasks/{id:guid}", async (
            Guid userId,
            Guid id,
            UpsertTaskRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var task = await service.UpdateTaskAsync(userId, id, request, cancellationToken);
            return task is null ? Results.NotFound() : Results.Ok(task);
        });

        group.MapDelete("/tasks/{id:guid}", async (
            Guid userId,
            Guid id,
            PlannerService service,
            CancellationToken cancellationToken) =>
            await service.DeleteTaskAsync(userId, id, cancellationToken)
                ? Results.NoContent()
                : Results.NotFound());

        group.MapGet("/habits", async (Guid userId, PlannerService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetHabitsAsync(userId, cancellationToken)));

        group.MapPost("/habits", async (
            Guid userId,
            UpsertHabitRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var habit = await service.CreateHabitAsync(userId, request, cancellationToken);
            return Results.Created($"/api/users/{userId}/habits/{habit.Id}", habit);
        });

        group.MapPut("/habits/{id:guid}", async (
            Guid userId,
            Guid id,
            UpsertHabitRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var habit = await service.UpdateHabitAsync(userId, id, request, cancellationToken);
            return habit is null ? Results.NotFound() : Results.Ok(habit);
        });

        group.MapDelete("/habits/{id:guid}", async (
            Guid userId,
            Guid id,
            PlannerService service,
            CancellationToken cancellationToken) =>
            await service.DeleteHabitAsync(userId, id, cancellationToken)
                ? Results.NoContent()
                : Results.NotFound());

        group.MapGet("/goals", async (Guid userId, PlannerService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetGoalsAsync(userId, cancellationToken)));

        group.MapPost("/goals", async (
            Guid userId,
            UpsertGoalRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var goal = await service.CreateGoalAsync(userId, request, cancellationToken);
            return Results.Created($"/api/users/{userId}/goals/{goal.Id}", goal);
        });

        group.MapPut("/goals/{id:guid}", async (
            Guid userId,
            Guid id,
            UpsertGoalRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var goal = await service.UpdateGoalAsync(userId, id, request, cancellationToken);
            return goal is null ? Results.NotFound() : Results.Ok(goal);
        });

        group.MapDelete("/goals/{id:guid}", async (
            Guid userId,
            Guid id,
            PlannerService service,
            CancellationToken cancellationToken) =>
            await service.DeleteGoalAsync(userId, id, cancellationToken)
                ? Results.NoContent()
                : Results.NotFound());

        group.MapGet("/notes", async (Guid userId, PlannerService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetNotesAsync(userId, cancellationToken)));

        group.MapPost("/notes", async (
            Guid userId,
            UpsertNoteRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var note = await service.CreateNoteAsync(userId, request, cancellationToken);
            return Results.Created($"/api/users/{userId}/notes/{note.Id}", note);
        });

        group.MapPut("/notes/{id:guid}", async (
            Guid userId,
            Guid id,
            UpsertNoteRequest request,
            PlannerService service,
            CancellationToken cancellationToken) =>
        {
            var note = await service.UpdateNoteAsync(userId, id, request, cancellationToken);
            return note is null ? Results.NotFound() : Results.Ok(note);
        });

        group.MapDelete("/notes/{id:guid}", async (
            Guid userId,
            Guid id,
            PlannerService service,
            CancellationToken cancellationToken) =>
            await service.DeleteNoteAsync(userId, id, cancellationToken)
                ? Results.NoContent()
                : Results.NotFound());

        return app;
    }
}

using RoadmApp.Domain.Planning;

namespace RoadmApp.Application.Planning;

public sealed record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    DateOnly? DueDate,
    ItemStatus Status,
    DateTime CreatedAt);

public sealed record UpsertTaskRequest(string Title, string? Description, DateOnly? DueDate, ItemStatus Status);

public sealed record HabitDto(
    Guid Id,
    string Title,
    string Frequency,
    int TargetCount,
    ItemStatus Status,
    DateTime CreatedAt);

public sealed record UpsertHabitRequest(string Title, string Frequency, int TargetCount, ItemStatus Status);

public sealed record GoalDto(
    Guid Id,
    string Title,
    string? Description,
    DateOnly? TargetDate,
    ItemStatus Status,
    DateTime CreatedAt);

public sealed record UpsertGoalRequest(string Title, string? Description, DateOnly? TargetDate, ItemStatus Status);

public sealed record NoteDto(Guid Id, string Title, string Content, DateTime CreatedAt);

public sealed record UpsertNoteRequest(string Title, string Content);

public sealed record DashboardDto(
    int PendingTasks,
    int CompletedTasks,
    int ActiveHabits,
    int ActiveGoals,
    int Notes,
    decimal WeeklyProgress);

using RoadmApp.Domain.Planning;

namespace RoadmApp.Application.Abstractions;

public interface IPlannerRepository
{
    Task<IReadOnlyList<RoadmapTask>> GetTasksAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<RoadmapTask?> GetTaskAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    void AddTask(RoadmapTask task);
    void RemoveTask(RoadmapTask task);

    Task<IReadOnlyList<Habit>> GetHabitsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Habit?> GetHabitAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    void AddHabit(Habit habit);
    void RemoveHabit(Habit habit);

    Task<IReadOnlyList<Goal>> GetGoalsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Goal?> GetGoalAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    void AddGoal(Goal goal);
    void RemoveGoal(Goal goal);

    Task<IReadOnlyList<Note>> GetNotesAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Note?> GetNoteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
    void AddNote(Note note);
    void RemoveNote(Note note);
}

using Microsoft.EntityFrameworkCore;
using RoadmApp.Application.Abstractions;
using RoadmApp.Domain.Planning;

namespace RoadmApp.Infrastructure.Persistence.Repositories;

public sealed class PlannerRepository(RoadmAppDbContext dbContext) : IPlannerRepository
{
    public async Task<IReadOnlyList<RoadmapTask>> GetTasksAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Tasks
            .AsNoTracking()
            .Where(task => task.UserId == userId)
            .OrderBy(task => task.DueDate)
            .ThenByDescending(task => task.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<RoadmapTask?> GetTaskAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Tasks.FirstOrDefaultAsync(task => task.UserId == userId && task.Id == id, cancellationToken);
    }

    public void AddTask(RoadmapTask task) => dbContext.Tasks.Add(task);

    public void RemoveTask(RoadmapTask task) => dbContext.Tasks.Remove(task);

    public async Task<IReadOnlyList<Habit>> GetHabitsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Habits
            .AsNoTracking()
            .Where(habit => habit.UserId == userId)
            .OrderByDescending(habit => habit.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Habit?> GetHabitAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Habits.FirstOrDefaultAsync(habit => habit.UserId == userId && habit.Id == id, cancellationToken);
    }

    public void AddHabit(Habit habit) => dbContext.Habits.Add(habit);

    public void RemoveHabit(Habit habit) => dbContext.Habits.Remove(habit);

    public async Task<IReadOnlyList<Goal>> GetGoalsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Goals
            .AsNoTracking()
            .Where(goal => goal.UserId == userId)
            .OrderBy(goal => goal.TargetDate)
            .ThenByDescending(goal => goal.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Goal?> GetGoalAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Goals.FirstOrDefaultAsync(goal => goal.UserId == userId && goal.Id == id, cancellationToken);
    }

    public void AddGoal(Goal goal) => dbContext.Goals.Add(goal);

    public void RemoveGoal(Goal goal) => dbContext.Goals.Remove(goal);

    public async Task<IReadOnlyList<Note>> GetNotesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Notes
            .AsNoTracking()
            .Where(note => note.UserId == userId)
            .OrderByDescending(note => note.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Note?> GetNoteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Notes.FirstOrDefaultAsync(note => note.UserId == userId && note.Id == id, cancellationToken);
    }

    public void AddNote(Note note) => dbContext.Notes.Add(note);

    public void RemoveNote(Note note) => dbContext.Notes.Remove(note);
}

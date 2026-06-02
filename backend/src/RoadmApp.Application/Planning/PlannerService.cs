using RoadmApp.Application.Abstractions;
using RoadmApp.Domain.Planning;

namespace RoadmApp.Application.Planning;

public sealed class PlannerService(IPlannerRepository planner, IUnitOfWork unitOfWork)
{
    public async Task<DashboardDto> GetDashboardAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tasks = await planner.GetTasksAsync(userId, cancellationToken);
        var habits = await planner.GetHabitsAsync(userId, cancellationToken);
        var goals = await planner.GetGoalsAsync(userId, cancellationToken);
        var notes = await planner.GetNotesAsync(userId, cancellationToken);

        var visibleTasks = tasks.Where(task => task.Status != ItemStatus.Archived).ToList();
        var completedTasks = visibleTasks.Count(task => task.Status == ItemStatus.Done);
        var progress = visibleTasks.Count == 0 ? 0 : decimal.Round(completedTasks * 100m / visibleTasks.Count, 2);

        return new DashboardDto(
            PendingTasks: visibleTasks.Count(task => task.Status is ItemStatus.Pending or ItemStatus.InProgress),
            CompletedTasks: completedTasks,
            ActiveHabits: habits.Count(habit => habit.Status != ItemStatus.Archived),
            ActiveGoals: goals.Count(goal => goal.Status != ItemStatus.Archived),
            Notes: notes.Count,
            WeeklyProgress: progress);
    }

    public async Task<IReadOnlyList<TaskDto>> GetTasksAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tasks = await planner.GetTasksAsync(userId, cancellationToken);
        return tasks.Select(MapTask).ToList();
    }

    public async Task<TaskDto> CreateTaskAsync(Guid userId, UpsertTaskRequest request, CancellationToken cancellationToken = default)
    {
        var task = new RoadmapTask(userId, request.Title, request.Description, request.DueDate);
        task.Update(request.Title, request.Description, request.DueDate, request.Status);
        planner.AddTask(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapTask(task);
    }

    public async Task<TaskDto?> UpdateTaskAsync(Guid userId, Guid id, UpsertTaskRequest request, CancellationToken cancellationToken = default)
    {
        var task = await planner.GetTaskAsync(userId, id, cancellationToken);
        if (task is null)
        {
            return null;
        }

        task.Update(request.Title, request.Description, request.DueDate, request.Status);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapTask(task);
    }

    public async Task<bool> DeleteTaskAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        var task = await planner.GetTaskAsync(userId, id, cancellationToken);
        if (task is null)
        {
            return false;
        }

        planner.RemoveTask(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<HabitDto>> GetHabitsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var habits = await planner.GetHabitsAsync(userId, cancellationToken);
        return habits.Select(MapHabit).ToList();
    }

    public async Task<HabitDto> CreateHabitAsync(Guid userId, UpsertHabitRequest request, CancellationToken cancellationToken = default)
    {
        var habit = new Habit(userId, request.Title, request.Frequency, request.TargetCount);
        habit.Update(request.Title, request.Frequency, request.TargetCount, request.Status);
        planner.AddHabit(habit);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapHabit(habit);
    }

    public async Task<HabitDto?> UpdateHabitAsync(Guid userId, Guid id, UpsertHabitRequest request, CancellationToken cancellationToken = default)
    {
        var habit = await planner.GetHabitAsync(userId, id, cancellationToken);
        if (habit is null)
        {
            return null;
        }

        habit.Update(request.Title, request.Frequency, request.TargetCount, request.Status);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapHabit(habit);
    }

    public async Task<bool> DeleteHabitAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        var habit = await planner.GetHabitAsync(userId, id, cancellationToken);
        if (habit is null)
        {
            return false;
        }

        planner.RemoveHabit(habit);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<GoalDto>> GetGoalsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var goals = await planner.GetGoalsAsync(userId, cancellationToken);
        return goals.Select(MapGoal).ToList();
    }

    public async Task<GoalDto> CreateGoalAsync(Guid userId, UpsertGoalRequest request, CancellationToken cancellationToken = default)
    {
        var goal = new Goal(userId, request.Title, request.Description, request.TargetDate);
        goal.Update(request.Title, request.Description, request.TargetDate, request.Status);
        planner.AddGoal(goal);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapGoal(goal);
    }

    public async Task<GoalDto?> UpdateGoalAsync(Guid userId, Guid id, UpsertGoalRequest request, CancellationToken cancellationToken = default)
    {
        var goal = await planner.GetGoalAsync(userId, id, cancellationToken);
        if (goal is null)
        {
            return null;
        }

        goal.Update(request.Title, request.Description, request.TargetDate, request.Status);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapGoal(goal);
    }

    public async Task<bool> DeleteGoalAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        var goal = await planner.GetGoalAsync(userId, id, cancellationToken);
        if (goal is null)
        {
            return false;
        }

        planner.RemoveGoal(goal);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<NoteDto>> GetNotesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var notes = await planner.GetNotesAsync(userId, cancellationToken);
        return notes.Select(MapNote).ToList();
    }

    public async Task<NoteDto> CreateNoteAsync(Guid userId, UpsertNoteRequest request, CancellationToken cancellationToken = default)
    {
        var note = new Note(userId, request.Title, request.Content);
        planner.AddNote(note);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapNote(note);
    }

    public async Task<NoteDto?> UpdateNoteAsync(Guid userId, Guid id, UpsertNoteRequest request, CancellationToken cancellationToken = default)
    {
        var note = await planner.GetNoteAsync(userId, id, cancellationToken);
        if (note is null)
        {
            return null;
        }

        note.Update(request.Title, request.Content);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapNote(note);
    }

    public async Task<bool> DeleteNoteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default)
    {
        var note = await planner.GetNoteAsync(userId, id, cancellationToken);
        if (note is null)
        {
            return false;
        }

        planner.RemoveNote(note);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static TaskDto MapTask(RoadmapTask task) =>
        new(task.Id, task.Title, task.Description, task.DueDate, task.Status, task.CreatedAt);

    private static HabitDto MapHabit(Habit habit) =>
        new(habit.Id, habit.Title, habit.Frequency, habit.TargetCount, habit.Status, habit.CreatedAt);

    private static GoalDto MapGoal(Goal goal) =>
        new(goal.Id, goal.Title, goal.Description, goal.TargetDate, goal.Status, goal.CreatedAt);

    private static NoteDto MapNote(Note note) =>
        new(note.Id, note.Title, note.Content, note.CreatedAt);
}

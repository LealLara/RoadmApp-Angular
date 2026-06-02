using RoadmApp.Domain.Common;

namespace RoadmApp.Domain.Users;

public sealed class User : Entity
{
    private readonly List<RoadmApp.Domain.Planning.RoadmapTask> _tasks = [];
    private readonly List<RoadmApp.Domain.Planning.Habit> _habits = [];
    private readonly List<RoadmApp.Domain.Planning.Goal> _goals = [];
    private readonly List<RoadmApp.Domain.Planning.Note> _notes = [];

    private User()
    {
    }

    public User(string name, string email, string passwordHash)
    {
        Rename(name);
        ChangeEmail(email);
        PasswordHash = passwordHash;
    }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public IReadOnlyCollection<RoadmApp.Domain.Planning.RoadmapTask> Tasks => _tasks;
    public IReadOnlyCollection<RoadmApp.Domain.Planning.Habit> Habits => _habits;
    public IReadOnlyCollection<RoadmApp.Domain.Planning.Goal> Goals => _goals;
    public IReadOnlyCollection<RoadmApp.Domain.Planning.Note> Notes => _notes;

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        Name = name.Trim();
        Touch();
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required.", nameof(email));
        }

        Email = email.Trim().ToLowerInvariant();
        Touch();
    }
}

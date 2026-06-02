using RoadmApp.Domain.Common;

namespace RoadmApp.Domain.Planning;

public sealed class RoadmapTask : Entity
{
    private RoadmapTask()
    {
    }

    public RoadmapTask(Guid userId, string title, string? description, DateOnly? dueDate)
    {
        UserId = userId;
        Update(title, description, dueDate, ItemStatus.Pending);
    }

    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateOnly? DueDate { get; private set; }
    public ItemStatus Status { get; private set; }

    public void Update(string title, string? description, DateOnly? dueDate, ItemStatus status)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        DueDate = dueDate;
        Status = status;
        Touch();
    }
}

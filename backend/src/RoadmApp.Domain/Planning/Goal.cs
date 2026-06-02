using RoadmApp.Domain.Common;

namespace RoadmApp.Domain.Planning;

public sealed class Goal : Entity
{
    private Goal()
    {
    }

    public Goal(Guid userId, string title, string? description, DateOnly? targetDate)
    {
        UserId = userId;
        Update(title, description, targetDate, ItemStatus.Pending);
    }

    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateOnly? TargetDate { get; private set; }
    public ItemStatus Status { get; private set; }

    public void Update(string title, string? description, DateOnly? targetDate, ItemStatus status)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        TargetDate = targetDate;
        Status = status;
        Touch();
    }
}

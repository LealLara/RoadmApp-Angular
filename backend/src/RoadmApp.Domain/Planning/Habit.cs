using RoadmApp.Domain.Common;

namespace RoadmApp.Domain.Planning;

public sealed class Habit : Entity
{
    private Habit()
    {
    }

    public Habit(Guid userId, string title, string frequency, int targetCount)
    {
        UserId = userId;
        Update(title, frequency, targetCount, ItemStatus.Pending);
    }

    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Frequency { get; private set; } = string.Empty;
    public int TargetCount { get; private set; }
    public ItemStatus Status { get; private set; }

    public void Update(string title, string frequency, int targetCount, ItemStatus status)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        if (targetCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetCount), "Target count must be greater than zero.");
        }

        Title = title.Trim();
        Frequency = string.IsNullOrWhiteSpace(frequency) ? "Daily" : frequency.Trim();
        TargetCount = targetCount;
        Status = status;
        Touch();
    }
}

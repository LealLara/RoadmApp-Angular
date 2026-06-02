using RoadmApp.Domain.Common;

namespace RoadmApp.Domain.Planning;

public sealed class Note : Entity
{
    private Note()
    {
    }

    public Note(Guid userId, string title, string content)
    {
        UserId = userId;
        Update(title, content);
    }

    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public void Update(string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Title = title.Trim();
        Content = string.IsNullOrWhiteSpace(content) ? string.Empty : content.Trim();
        Touch();
    }
}

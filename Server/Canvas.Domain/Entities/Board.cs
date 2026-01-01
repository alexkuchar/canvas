using Canvas.Domain.Exceptions;

namespace Canvas.Domain.Entities;

public class Board
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; } = null;

    public User User { get; set; } = null!;
    public Guid UserId { get; private set; }

    public DateTime UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Board() { }

    public Board(
        string title,
        string? description,
        User user,
        Guid userId
    )
    {
        Id = Guid.NewGuid();
        Title = AssertIsValidTitle(title);
        Description = description;
        User = user;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBoard(string title, string? description)
    {
        Title = AssertIsValidTitle(title);
        Description = description;

        Touch();
    }

    public string AssertIsValidTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new InvalidBoardTitleException(title);
        if (title.Length > 100) throw new InvalidBoardTitleException(title);

        return title.Trim();
    }

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
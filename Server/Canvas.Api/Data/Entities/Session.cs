namespace Canvas.Api.Data.Entities;

public class Session
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool IsRevoked { get; set; }
    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    private Session()
    {

    }

    public Session(
        Guid userId,
        string token,
        DateTime expiresAt
    )
    {
        UserId = userId;
        Token = token ?? throw new ArgumentNullException(nameof(token));
        IsRevoked = false;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
    }

    public void revoke()
    {
        IsRevoked = true;
    }

    public bool isExpired()
    {
        return DateTime.UtcNow >= ExpiresAt;
    }
}
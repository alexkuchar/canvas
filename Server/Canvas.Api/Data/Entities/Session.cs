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
}
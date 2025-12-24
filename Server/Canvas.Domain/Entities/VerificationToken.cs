using Canvas.Domain.Exceptions;

namespace Canvas.Domain.Entities;

public class VerificationToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;
    public bool IsUsed { get; set; } = false;
    public DateTime ExpiresAt { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private VerificationToken() { }

    public VerificationToken(string token, Guid userId, DateTime expiresAt)
    {
        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
        Touch();
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiresAt;
    }

    public bool IsTokenUsed()
    {
        return IsUsed;
    }

    public void Use()
    {
        if (IsTokenUsed()) throw new TokenAlreadyUsedException();
        if (IsExpired()) throw new TokenExpiredException();

        IsUsed = true;

        Touch();
    }

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
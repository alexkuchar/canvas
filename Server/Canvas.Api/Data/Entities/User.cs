using System.ComponentModel.DataAnnotations;
using System.Data;
using Canvas.Api.Data.Exceptions;

namespace Canvas.Api.Data.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<Session> Sessions { get; private set; } = new List<Session>();

    private User() { }

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash
    )
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));

        AssertIsValidEmail(email);

        PasswordHash = passwordHash ?? throw new InvalidPasswordHashException();

        Id = Guid.NewGuid();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeEmail(string email)
    {
        if (Email == email) throw new EmailUnchangedException();

        AssertIsValidEmail(email);

        Touch();
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash ?? throw new InvalidPasswordHashException();
        Touch();
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Touch();
    }

    public void AssertIsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new InvalidEmailException(email);

        if (!new EmailAddressAttribute().IsValid(email)) throw new InvalidEmailException(email);

        Email = email.Trim().ToLowerInvariant();
    }

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        Touch();
    }
}
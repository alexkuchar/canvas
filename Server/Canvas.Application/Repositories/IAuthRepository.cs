using Canvas.Domain.Entities;

namespace Canvas.Application.Repositories;

public interface IAuthRepository
{
    Task<Session?> GetSessionByRefreshTokenAsync(string token);

    Task AddSessionAsync(Session session);
    Task RevokeSessionAsync(Session session);
    Task RevokeAllSessionsAsync(Guid userId);
}
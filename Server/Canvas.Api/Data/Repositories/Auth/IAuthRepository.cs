using Canvas.Api.Data.Entities;

namespace Canvas.Api.Data.Repositories;

public interface IAuthRepository
{
    Task<Session?> GetSessionByRefreshTokenAsync(string token);

    Task AddSessionAsync(Session session);
    Task RevokeSessionAsync(Session session);
    Task RevokeAllSessionsAsync(Guid userId);
}
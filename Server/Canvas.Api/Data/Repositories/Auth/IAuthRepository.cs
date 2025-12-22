using Canvas.Api.Data.Entities;

namespace Canvas.Api.Data.Repositories;

public interface IAuthRepository
{
    Task<Session?> GetSessionByRefreshTokenAsync(string token);

    Task AddSession(Session session);
    Task RevokeSession(Session session);
    Task RevokeAllSessions(Guid userId);
}
using System.Threading.Tasks;
using Canvas.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canvas.Api.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetSessionByRefreshTokenAsync(string token)
    {
        return await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
    }

    public async Task AddSession(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }

    public async Task RevokeAllSessions(Guid userId)
    {
        await _context.Sessions.Where(s => s.UserId == userId)
        .ExecuteUpdateAsync(s => s.SetProperty(s => s.IsRevoked, true));
    }

    public Task RevokeSession(Session session)
    {
        session.IsRevoked = true;
        return Task.CompletedTask;
    }
}
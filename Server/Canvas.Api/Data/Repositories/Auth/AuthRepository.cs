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
        return await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Token == token);
    }

    public async Task AddSessionAsync(Session session)
    {
        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task RevokeAllSessionsAsync(Guid userId)
    {
        await _context.Sessions.Where(s => s.UserId == userId)
        .ExecuteUpdateAsync(s => s.SetProperty(s => s.IsRevoked, true));
        await _context.SaveChangesAsync();
    }

    public async Task RevokeSessionAsync(Session session)
    {
        session.IsRevoked = true;
        await _context.SaveChangesAsync();
    }
}
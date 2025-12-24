using Canvas.Application.Repositories;
using Canvas.Domain.Entities;
using Canvas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Canvas.Infrastructure.Repositories;

public class VerificationTokenRepository : IVerificationTokenRepository
{

    private readonly AppDbContext _context;

    public VerificationTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddVerificationTokenAsync(VerificationToken verificationToken)
    {
        await _context.VerificationTokens.AddAsync(verificationToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVerificationTokenAsync(VerificationToken verificationToken)
    {
        _context.VerificationTokens.Remove(verificationToken);
        await _context.SaveChangesAsync();
    }

    public async Task<VerificationToken?> GetVerificationTokenByTokenAsync(string token)
    {
        return await _context.VerificationTokens.FirstOrDefaultAsync(vt => vt.Token == token);
    }

    public async Task<VerificationToken?> GetVerificationTokenByUserIdAsync(Guid userId)
    {
        return await _context.VerificationTokens.FirstOrDefaultAsync(vt => vt.UserId == userId);
    }
}

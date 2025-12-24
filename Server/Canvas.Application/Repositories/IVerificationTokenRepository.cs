using Canvas.Domain.Entities;

namespace Canvas.Application.Repositories;

public interface IVerificationTokenRepository
{
    Task<VerificationToken?> GetVerificationTokenByTokenAsync(string token);
    Task<VerificationToken?> GetVerificationTokenByUserIdAsync(Guid userId);
    Task AddVerificationTokenAsync(VerificationToken verificationToken);
    Task DeleteVerificationTokenAsync(VerificationToken verificationToken);
}
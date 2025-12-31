using System.Reflection.Emit;
using System.Security.Cryptography;
using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Domain.Entities;
using Canvas.Domain.Enums;
using Canvas.Domain.Exceptions;

namespace Canvas.Application.Auth.Handlers;

public class ResendVerificationEmailHandler
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVerificationTokenRepository _verificationTokenRepository;
    private readonly IEmailRepository _emailRepository;
    public ResendVerificationEmailHandler(IAuthRepository authRepository, IUserRepository userRepository, IVerificationTokenRepository verificationTokenRepository, IEmailRepository emailRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _verificationTokenRepository = verificationTokenRepository;
        _emailRepository = emailRepository;
    }

    public async Task HandleAsync(ResendVerificationEmailCommand command)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email) ?? throw new UserNotFoundException(command.Email);

        if (user.IsUserVerified()) throw new UserAlreadyVerifiedException();
        if (user.LastVerificationEmailSentAt is not null && user.LastVerificationEmailSentAt.Value.AddMinutes(1) > DateTime.UtcNow) throw new VerificationEmailRateLimitException((int)(user.LastVerificationEmailSentAt.Value.AddMinutes(1) - DateTime.UtcNow).TotalSeconds);

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).Replace('+', '-').Replace('/', '_').TrimEnd('=');

        var verificationToken = new VerificationToken(
            token: token,
            userId: user.Id,
            expiresAt: DateTime.UtcNow.AddMinutes(15),
            verificationTokenType: VerificationTokenType.EmailVerification
        );

        await _verificationTokenRepository.AddVerificationTokenAsync(verificationToken);

        await _emailRepository.SendVerificationEmailAsync(
            email: command.Email,
            firstName: user.FirstName,
            verificationToken: verificationToken.Token
        );

        user.LastVerificationEmailSentAt = DateTime.UtcNow;
        user.Touch();

        await _userRepository.UpdateUserAsync(user);
    }
}
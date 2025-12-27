using System.Security.Cryptography;
using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Domain.Entities;
using Canvas.Domain.Enums;

namespace Canvas.Application.Auth.Handlers;

public class ForgotPasswordHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailRepository _emailRepository;
    private readonly IVerificationTokenRepository _verificationTokenRepository;

    public ForgotPasswordHandler(IUserRepository userRepository, IEmailRepository emailRepository, IVerificationTokenRepository verificationTokenRepository)
    {
        _userRepository = userRepository;
        _emailRepository = emailRepository;
        _verificationTokenRepository = verificationTokenRepository;
    }

    public async Task HandleAsync(ForgotPasswordCommand command)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email);
        if (user == null) throw new UserNotFoundException(command.Email);

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        var forgotPasswordToken = new VerificationToken(
            userId: user.Id,
            token: token,
            expiresAt: DateTime.UtcNow.AddMinutes(15),
            verificationTokenType: VerificationTokenType.PasswordReset
        );

        await _verificationTokenRepository.AddVerificationTokenAsync(forgotPasswordToken);

        await _emailRepository.SendPasswordResetEmailAsync(
            email: command.Email,
            firstName: user.FirstName,
            passwordResetToken: forgotPasswordToken.Token
        );
    }
}
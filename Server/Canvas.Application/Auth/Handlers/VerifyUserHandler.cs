using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Results;
using Canvas.Domain.Enums;

namespace Canvas.Application.Auth.Handlers;

public class VerifyUserHandler
{
    private readonly IVerificationTokenRepository _verificationTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IEmailRepository _emailRepository;

    public VerifyUserHandler(IVerificationTokenRepository verificationTokenRepository, IUserRepository userRepository, IAuthRepository authRepository, IEmailRepository emailRepository)
    {
        _verificationTokenRepository = verificationTokenRepository;
        _userRepository = userRepository;
        _authRepository = authRepository;
        _emailRepository = emailRepository;
    }

    public async Task HandleAsync(VerifyUserCommand command)
    {
        var token = await _verificationTokenRepository.GetVerificationTokenByTokenAsync(command.VerificationToken) ?? throw new VerificationTokenNotFoundException();

        if (token.IsExpired()) throw new VerificationTokenExpiredException();
        if (token.IsTokenUsed()) throw new VerificationTokenUsedException();

        if (token.VerificationTokenType != VerificationTokenType.EmailVerification) throw new VerificationTokenTypeMismatchException();

        token.Use();
        await _verificationTokenRepository.DeleteVerificationTokenAsync(token);

        var user = await _userRepository.GetUserByIdAsync(token.UserId) ?? throw new UserNotFoundException(token.UserId);

        user.Verify();

        await _userRepository.UpdateUserAsync(user);

        await _emailRepository.SendWelcomeEmailAsync(user.Email, user.FirstName);
    }
}
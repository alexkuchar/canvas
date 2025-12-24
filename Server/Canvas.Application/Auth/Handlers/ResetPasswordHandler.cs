using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Domain.Enums;

namespace Canvas.Application.Auth.Handlers;

public class ResetPasswordHandler
{
    private readonly IVerificationTokenRepository _verificationTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordHandler(IVerificationTokenRepository verificationTokenRepository, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _verificationTokenRepository = verificationTokenRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(ResetPasswordCommand command)
    {
        var token = await _verificationTokenRepository.GetVerificationTokenByTokenAsync(command.Token) ?? throw new VerificationTokenNotFoundException();

        if (token.IsExpired()) throw new VerificationTokenExpiredException();
        if (token.IsTokenUsed()) throw new VerificationTokenUsedException();

        if (token.VerificationTokenType != VerificationTokenType.PasswordReset) throw new VerificationTokenTypeMismatchException();

        token.Use();

        var user = await _userRepository.GetUserByIdAsync(token.UserId) ?? throw new UserNotFoundException(token.UserId);

        user.ChangePassword(_passwordHasher.Hash(command.NewPassword));

        await _userRepository.UpdateUserAsync(user);

        await _verificationTokenRepository.DeleteVerificationTokenAsync(token);
    }
}
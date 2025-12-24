using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Results;

namespace Canvas.Application.Auth.Handlers;

public class VerifyUserHandler
{
    private readonly IVerificationTokenRepository _verificationTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;

    public VerifyUserHandler(IVerificationTokenRepository verificationTokenRepository, IUserRepository userRepository, IAuthRepository authRepository)
    {
        _verificationTokenRepository = verificationTokenRepository;
        _userRepository = userRepository;
        _authRepository = authRepository;
    }

    public async Task HandleAsync(VerifyUserCommand command)
    {
        var token = await _verificationTokenRepository.GetVerificationTokenByTokenAsync(command.VerificationToken) ?? throw new VerificationTokenNotFoundException();

        if (token.IsExpired()) throw new VerificationTokenExpiredException();
        if (token.IsTokenUsed()) throw new VerificationTokenUsedException();

        token.Use();
        await _verificationTokenRepository.DeleteVerificationTokenAsync(token);

        var user = await _userRepository.GetUserByIdAsync(token.UserId) ?? throw new UserNotFoundException(token.UserId);

        user.Verify();

        await _userRepository.UpdateUserAsync(user);
    }
}
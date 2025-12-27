using System.Security.Cryptography;
using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Domain.Entities;
using Canvas.Domain.Enums;

namespace Canvas.Application.Auth.Handlers;

public class RegisterUserHandler
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IVerificationTokenRepository _verificationTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailRepository _emailRepository;
    public RegisterUserHandler(IAuthRepository authRepository, IUserRepository userRepository, IVerificationTokenRepository verificationTokenRepository, IPasswordHasher passwordHasher, IEmailRepository emailRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _verificationTokenRepository = verificationTokenRepository;
        _passwordHasher = passwordHasher;
        _emailRepository = emailRepository;
    }

    public async Task<User> HandleAsync(RegisterUserCommand command)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
        if (existingUser != null) throw new EmailAlreadyInUseException(command.Email);

        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.FirstName,
            command.LastName,
            command.Email,
            passwordHash
        );

        await _userRepository.AddUserAsync(user);

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).Replace('+', '-').Replace('/', '_').TrimEnd('=');

        var verificationToken = new VerificationToken(
            userId: user.Id,
            token: token,
            expiresAt: DateTime.UtcNow.AddMinutes(15),
            verificationTokenType: VerificationTokenType.EmailVerification
        );

        await _verificationTokenRepository.AddVerificationTokenAsync(verificationToken);

        // TODO: Replace with actual URI when frontend is ready. This is a temporary solution.
        await _emailRepository.SendVerificationEmailAsync(
            email: command.Email,
            firstName: command.FirstName,
            verificationToken: verificationToken.Token
        );

        return user;
    }
}
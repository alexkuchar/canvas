using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Domain.Entities;

namespace Canvas.Application.Auth.Handlers;

public class RegisterUserHandler
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(IAuthRepository authRepository, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
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

        return user;
    }
}
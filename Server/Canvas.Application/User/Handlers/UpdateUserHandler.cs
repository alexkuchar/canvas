using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Repositories;
using Canvas.Application.User.Commands;
using Canvas.Application.Security;

namespace Canvas.Application.User.Handlers;

public class UpdateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(UpdateUserCommand command)
    {
        var user = await _userRepository.GetUserByIdAsync(command.Id) ?? throw new UserNotFoundException(command.Id);

        if (command.FirstName != null || command.LastName != null)
        {
            user.ChangeName(
                command.FirstName ?? user.FirstName,
                command.LastName ?? user.LastName
            );
        }

        if (command.Email != null && command.Email != user.Email)
        {
            user.ChangeEmail(command.Email);
        }

        if (command.Password != null)
        {
            user.ChangePassword(_passwordHasher.Hash(command.Password));
        }

        await _userRepository.UpdateUserAsync(user: user);
    }
}
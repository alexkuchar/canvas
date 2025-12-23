using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Application.Exceptions;

namespace Canvas.Application.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task ChangeEmailAsync(Guid id, string email)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        if (await _userRepository.GetUserByEmailAsync(email) != null) throw new EmailAlreadyInUseException(email);

        user.ChangeEmail(email);

    }

    public async Task ChangeNameAsync(Guid id, string firstName, string lastName)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        user.ChangeName(firstName, lastName);

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task ChangePasswordAsync(Guid id, string password)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        var passwordHash = _passwordHasher.Hash(password);

        user.ChangePassword(passwordHash);

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeactivateAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        user.Deactivate();

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        await _userRepository.DeleteUserAsync(id);
    }
}

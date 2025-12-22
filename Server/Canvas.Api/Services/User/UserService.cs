using Canvas.Api.Data;
using Canvas.Api.Data.DTOs;
using Canvas.Api.Data.Repositories;
using Canvas.Api.Data.Entities;
using Canvas.Api.Services.User.Exceptions;

namespace Canvas.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly AppDbContext _context;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, AppDbContext context)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _context = context;
    }

    public async Task ChangeEmailAsync(Guid id, string email)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        if (await _userRepository.GetUserByEmailAsync(email) != null) throw new EmailAlreadyInUseException(email);

        user.ChangeEmail(email);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeNameAsync(Guid id, string firstName, string lastName)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        user.ChangeName(firstName, lastName);

        await _userRepository.UpdateUserAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(Guid id, string password)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        var passwordHash = _passwordHasher.Hash(password);

        user.ChangePassword(passwordHash);

        await _userRepository.UpdateUserAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeactivateAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        user.Deactivate();

        await _userRepository.UpdateUserAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException(id);

        await _userRepository.DeleteUserAsync(id);
        await _context.SaveChangesAsync();
    }
}

using Canvas.Api.Data;
using Canvas.Api.Data.Repositories;
using Canvas.Api.Services.Auth.Commands;
using Canvas.Api.Services.User.Exceptions;

namespace Canvas.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context, IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _context = context;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<Data.Entities.User> RegisterAsync(RegisterUserCommand registerCommand)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(registerCommand.Email);
        if (existingUser != null) throw new EmailAlreadyInUseException(registerCommand.Email);

        var passwordHash = _passwordHasher.Hash(registerCommand.Password);

        var user = new Data.Entities.User(
            registerCommand.FirstName,
            registerCommand.LastName,
            registerCommand.Email,
            passwordHash
        );

        await _userRepository.AddUserAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

}
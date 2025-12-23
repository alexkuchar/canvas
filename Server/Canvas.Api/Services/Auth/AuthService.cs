using Canvas.Api.Data.Repositories;
using Canvas.Api.Services.Auth.Commands;
using Canvas.Api.Services.Auth.Exceptions;
using Canvas.Api.Services.Auth.Options;
using Canvas.Api.Services.Auth.Results;
using Canvas.Api.Services.User.Exceptions;
using Microsoft.Extensions.Options;

namespace Canvas.Api.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions _jwtOptions;
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IOptions<JwtOptions> jwtOptions, IAuthRepository authRepository, IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _jwtOptions = jwtOptions.Value;
        _authRepository = authRepository;
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

        return user;
    }

    public async Task<AuthResult> LoginAsync(LoginUserCommand loginCommand)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginCommand.Email) ?? throw new UserNotFoundException(loginCommand.Email);

        if (!_passwordHasher.Verify(loginCommand.Password, user.PasswordHash)) throw new InvalidPasswordException();

        var accessToken = _tokenService.CreateAccessToken(user.Id.ToString(), user.Email);

        var refreshToken = _tokenService.CreateRefreshToken(user.Id.ToString());
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);

        await _authRepository.AddSessionAsync(new Data.Entities.Session(
            userId: user.Id,
            token: refreshToken,
            expiresAt: refreshTokenExpiresAt
        ));

        return new AuthResult(
            UserId: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Email: user.Email,
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            RefreshTokenExpiresAt: refreshTokenExpiresAt
        );
    }

}
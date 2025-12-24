using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Options;
using Canvas.Application.Repositories;
using Canvas.Application.Results;
using Canvas.Application.Security;
using Microsoft.Extensions.Options;
using Canvas.Domain.Entities;

namespace Canvas.Application.Auth.Handlers;

public class LoginUserHandler
{

    private readonly JwtOptions _jwtOptions;
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;


    public LoginUserHandler(IOptions<JwtOptions> jwtOptions, IAuthRepository authRepository, IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _jwtOptions = jwtOptions.Value;
        _authRepository = authRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResult> HandleAsync(LoginUserCommand command)
    {
        var user = await _userRepository.GetUserByEmailAsync(command.Email) ?? throw new UserNotFoundException(command.Email);

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash)) throw new InvalidPasswordException();

        var accessToken = _tokenService.CreateAccessToken(user.Id.ToString(), user.Email);

        var refreshToken = _tokenService.CreateRefreshToken(user.Id.ToString());
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);

        await _authRepository.AddSessionAsync(new Session(
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
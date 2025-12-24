using Canvas.Domain.Entities;
using Canvas.Application.Auth.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Exceptions;
using Canvas.Application.Options;
using Canvas.Application.Repositories;
using Canvas.Application.Results;
using Canvas.Application.Security;
using Microsoft.Extensions.Options;

namespace Canvas.Application.Auth.Handlers;

public class RefreshSessionHandler
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;


    public RefreshSessionHandler(IAuthRepository authRepository, ITokenService tokenService, IOptions<JwtOptions> jwtOptions)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<SessionRefreshResult> HandleAsync(RefreshSessionCommand command)
    {
        var session = await _authRepository.GetSessionByRefreshTokenAsync(command.RefreshToken) ?? throw new SessionNotFoundException(command.RefreshToken);

        if (session.isExpired()) throw new SessionExpiredException();
        if (session.isRevoked()) throw new SessionRevokedException();


        await _authRepository.RevokeSessionAsync(session);

        var accessToken = _tokenService.CreateAccessToken(session.UserId.ToString(), session.User.Email);

        var refreshToken = _tokenService.CreateRefreshToken(session.UserId.ToString());
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);

        await _authRepository.AddSessionAsync(new Session(
            userId: session.UserId,
            token: refreshToken,
            expiresAt: refreshTokenExpiresAt
        ));

        return new SessionRefreshResult(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            RefreshTokenExpiresAt: refreshTokenExpiresAt
        );
    }
}
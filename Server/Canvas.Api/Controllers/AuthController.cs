using Canvas.Api.Services;
using Canvas.Api.Data.DTOs;
using Canvas.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Canvas.Application.Exceptions;
using Canvas.Application.Services.Auth;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var command = new RegisterUserCommand
        (
            registerRequestDto.FirstName,
            registerRequestDto.LastName,
            registerRequestDto.Email,
            registerRequestDto.Password
        );

        var user = await _authService.RegisterAsync(command);

        var responseDto = new RegisterResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        return CreatedAtAction(nameof(Register), responseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var command = new LoginUserCommand(
            loginRequest.Email,
            loginRequest.Password
        );

        var result = await _authService.LoginAsync(command);

        var responseDto = new LoginResponseDto(
            User: new UserDto(
                Id: result.UserId,
                FirstName: result.FirstName,
                LastName: result.LastName,
                Email: result.Email
            ),
            Tokens: new TokenPairDto(
                AccessToken: result.AccessToken,
                RefreshToken: result.RefreshToken,
                RefreshTokenExpiresAt: result.RefreshTokenExpiresAt
            )
        );

        return Ok(responseDto);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto refreshRequest)
    {
        var command = new RefreshSessionCommand(
            refreshRequest.RefreshToken
        );

        var result = await _authService.RefreshSessionAsync(command);

        var responseDto = new RefreshResponseDto(
            AccessToken: result.AccessToken,
            RefreshToken: result.RefreshToken,
            RefreshTokenExpiresAt: result.RefreshTokenExpiresAt
        );

        return Ok(responseDto);
    }
}
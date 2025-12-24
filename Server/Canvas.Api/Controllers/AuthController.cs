using Canvas.Api.DTOs;
using Canvas.Application.Auth.Commands;
using Microsoft.AspNetCore.Mvc;
using Canvas.Application.Auth.Handlers;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginUserHandler _loginUserHandler;
    private readonly RefreshSessionHandler _refreshSessionHandler;
    private readonly VerifyUserHandler _verifyUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler, RefreshSessionHandler refreshSessionHandler, VerifyUserHandler verifyUserHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
        _refreshSessionHandler = refreshSessionHandler;
        _verifyUserHandler = verifyUserHandler;
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

        var user = await _registerUserHandler.HandleAsync(command);

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

        var result = await _loginUserHandler.HandleAsync(command);

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

        var result = await _refreshSessionHandler.HandleAsync(command);

        var responseDto = new RefreshResponseDto(
            AccessToken: result.AccessToken,
            RefreshToken: result.RefreshToken,
            RefreshTokenExpiresAt: result.RefreshTokenExpiresAt
        );

        return Ok(responseDto);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyRequestDto verifyRequest)
    {
        var command = new VerifyUserCommand(
            verifyRequest.VerificationToken
        );

        await _verifyUserHandler.HandleAsync(command);

        var responseDto = new VerifyResponseDto(
            Message: "User verified successfully"
        );

        return Ok(responseDto);
    }
}
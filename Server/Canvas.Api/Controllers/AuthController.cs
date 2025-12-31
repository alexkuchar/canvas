using Canvas.Api.DTOs;
using Canvas.Application.Auth.Commands;
using Microsoft.AspNetCore.Mvc;
using Canvas.Application.Auth.Handlers;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginUserHandler _loginUserHandler;
    private readonly RefreshSessionHandler _refreshSessionHandler;
    private readonly VerifyUserHandler _verifyUserHandler;
    private readonly ForgotPasswordHandler _forgotPasswordHandler;
    private readonly ResetPasswordHandler _resetPasswordHandler;
    private readonly ResendVerificationEmailHandler _resendVerificationEmailHandler;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler, RefreshSessionHandler refreshSessionHandler, VerifyUserHandler verifyUserHandler, ForgotPasswordHandler forgotPasswordHandler, ResetPasswordHandler resetPasswordHandler, ResendVerificationEmailHandler resendVerificationEmailHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
        _refreshSessionHandler = refreshSessionHandler;
        _verifyUserHandler = verifyUserHandler;
        _forgotPasswordHandler = forgotPasswordHandler;
        _resetPasswordHandler = resetPasswordHandler;
        _resendVerificationEmailHandler = resendVerificationEmailHandler;
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequest)
    {
        var command = new ForgotPasswordCommand(
            forgotPasswordRequest.Email
        );

        await _forgotPasswordHandler.HandleAsync(command);

        return Ok(new { Message = "Password reset email sent" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequest)
    {
        var command = new ResetPasswordCommand(
            resetPasswordRequest.Token,
            resetPasswordRequest.NewPassword
        );

        await _resetPasswordHandler.HandleAsync(command);

        return Ok(new { Message = "Password reset successfully" });
    }

    [HttpPost("resend-verification-email")]
    public async Task<IActionResult> ResendVerificationEmail([FromBody] ResendVerificationEmailDto resendVerificationEmailDto)
    {
        var command = new ResendVerificationEmailCommand(
            resendVerificationEmailDto.Email
        );

        await _resendVerificationEmailHandler.HandleAsync(command);

        return Ok(new { Message = "Verification email resent successfully" });
    }
}
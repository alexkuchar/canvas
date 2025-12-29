using Canvas.Api.DTOs;
using Canvas.Application.User.Commands;
using Canvas.Application.User.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UpdateUserHandler _updateUserHandler;

    public UserController(UpdateUserHandler updateUserHandler)
    {
        _updateUserHandler = updateUserHandler;
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequestDto updateUserRequestDto)
    {
        var command = new UpdateUserCommand(
            Id: updateUserRequestDto.Id,
            FirstName: updateUserRequestDto.FirstName,
            LastName: updateUserRequestDto.LastName,
            Email: updateUserRequestDto.Email,
            Password: updateUserRequestDto.Password
        );

        await _updateUserHandler.HandleAsync(command);

        return Ok(new UpdateUserResponseDto(
            Id: command.Id,
            FirstName: command.FirstName,
            LastName: command.LastName,
            Email: command.Email
        ));
    }

}
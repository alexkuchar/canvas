using Canvas.Api.Data.DTOs;
using Canvas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var user = await _userService.CreateAsync(registerUserDto);

        var responseDto = new RegisterResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        return Ok(responseDto);
    }
}

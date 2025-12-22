using Canvas.Api.Data.DTOs;
using Canvas.Api.Services.Auth.Commands;

namespace Canvas.Api.Services;

public interface IAuthService
{
    Task<Data.Entities.User> RegisterAsync(RegisterUserCommand registerCommand);
}
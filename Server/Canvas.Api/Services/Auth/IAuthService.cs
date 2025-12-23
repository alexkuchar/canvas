using Canvas.Api.Services.Auth.Commands;
using Canvas.Api.Services.Auth.Results;

namespace Canvas.Api.Services;

public interface IAuthService
{
    Task<Data.Entities.User> RegisterAsync(RegisterUserCommand registerCommand);
    Task<AuthResult> LoginAsync(LoginUserCommand loginCommand);
}
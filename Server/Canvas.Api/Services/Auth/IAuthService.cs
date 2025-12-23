using Canvas.Api.Services.Auth.Commands;
using Canvas.Api.Services.Auth.Results;

namespace Canvas.Api.Services;

public interface IAuthService
{
Task<Canvas.Domain.Entities.User> RegisterAsync(RegisterUserCommand registerCommand);
    Task<AuthResult> LoginAsync(LoginUserCommand loginCommand);
    Task<SessionRefreshResult> RefreshSessionAsync(RefreshSessionCommand refreshSessionCommand);

}
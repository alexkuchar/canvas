using Canvas.Application.Commands;
using Canvas.Application.Results;

namespace Canvas.Application.Services.Auth;

public interface IAuthService
{
    Task<Canvas.Domain.Entities.User> RegisterAsync(RegisterUserCommand registerCommand);
    Task<AuthResult> LoginAsync(LoginUserCommand loginCommand);
    Task<SessionRefreshResult> RefreshSessionAsync(RefreshSessionCommand refreshSessionCommand);

}
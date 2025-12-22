using Canvas.Api.Data.DTOs;

namespace Canvas.Api.Services;

public interface IUserService
{
    Task ChangeEmailAsync(Guid id, string email);
    Task ChangePasswordAsync(Guid id, string password);
    Task ChangeNameAsync(Guid id, string firstName, string lastName);
    Task DeactivateAsync(Guid id);
    Task DeleteAsync(Guid id);
}
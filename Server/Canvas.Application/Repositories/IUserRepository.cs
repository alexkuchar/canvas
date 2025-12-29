using Canvas.Domain.Entities;

namespace Canvas.Application.Repositories;

public interface IUserRepository
{
    Task<Domain.Entities.User?> GetUserByIdAsync(Guid id);
    Task<Domain.Entities.User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(Domain.Entities.User user);
    Task UpdateUserAsync(Domain.Entities.User user);
    Task DeleteUserAsync(Guid id);
}
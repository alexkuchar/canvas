using Canvas.Domain.Entities;

namespace Canvas.Api.Data.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
}
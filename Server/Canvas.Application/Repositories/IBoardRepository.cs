using Canvas.Domain.Entities;

namespace Canvas.Application.Repositories;

public interface IBoardRepository
{
    Task<Domain.Entities.Board?> GetBoardByIdAsync(Guid id);
    Task<Domain.Entities.Board?> GetBoardByTitleAsync(string title);
    Task<ICollection<Domain.Entities.Board>> GetBoardsByUserIdAsync(Guid userId);
    Task AddBoardAsync(Domain.Entities.Board board);
    Task UpdateBoardAsync(Domain.Entities.Board board);
    Task DeleteBoardAsync(Guid id);
}
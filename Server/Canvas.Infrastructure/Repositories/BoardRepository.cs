using Canvas.Application.Repositories;
using Canvas.Domain.Entities;
using Canvas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Canvas.Infrastructure.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly AppDbContext _context;

    public BoardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Board?> GetBoardByIdAsync(Guid id)
    {
        return await _context.Boards.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Board?> GetBoardByTitleAsync(string title)
    {
        return await _context.Boards.Include(b => b.User).FirstOrDefaultAsync(b => b.Title == title);
    }

    public async Task<ICollection<Board>> GetBoardsByUserIdAsync(Guid userId)
    {
        return await _context.Boards.Include(b => b.User).Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task AddBoardAsync(Board board)
    {
        await _context.Boards.AddAsync(board);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBoardAsync(Board board)
    {
        _context.Boards.Update(board);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBoardAsync(Guid id)
    {
        await _context.Boards.Where(b => b.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}
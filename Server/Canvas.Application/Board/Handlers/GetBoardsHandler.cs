using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Board.Commands;
using Canvas.Application.Repositories;

namespace Canvas.Application.Board.Handlers;

public class GetBoardsHandler
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserRepository _userRepository;

    public GetBoardsHandler(IBoardRepository boardRepository, IUserRepository userRepository)
    {
        _boardRepository = boardRepository;
        _userRepository = userRepository;
    }

    public async Task<ICollection<Domain.Entities.Board>> HandleAsync(GetBoardsCommand getBoardsCommand)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(getBoardsCommand.userId);
        if (existingUser is null) throw new UserNotFoundException(getBoardsCommand.userId);

        var boards = await _boardRepository.GetBoardsByUserIdAsync(getBoardsCommand.userId);

        return boards;
    }

}
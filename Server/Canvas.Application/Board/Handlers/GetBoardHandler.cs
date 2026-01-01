using Canvas.Application.Board.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Board.Exceptions;
using Canvas.Application.Repositories;

namespace Canvas.Application.Board.Handlers;

public class GetBoardHandler
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserRepository _userRepository;

    public GetBoardHandler(IBoardRepository boardRepository, IUserRepository userRepository)
    {
        _boardRepository = boardRepository;
        _userRepository = userRepository;
    }

    public async Task<Domain.Entities.Board> HandleAsync(GetBoardCommand getBoardCommand)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(getBoardCommand.userId);
        if (existingUser is null) throw new UserNotFoundException(getBoardCommand.userId);

        var board = await _boardRepository.GetBoardByIdAsync(getBoardCommand.boardId);
        if (board is null) throw new BoardNotFoundException(getBoardCommand.boardId);

        if (board.UserId != getBoardCommand.userId) throw new BoardNotOwnedByUserException(getBoardCommand.boardId);

        return board;
    }
}
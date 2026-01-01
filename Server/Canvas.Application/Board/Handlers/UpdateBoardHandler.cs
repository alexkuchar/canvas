using Canvas.Application.Repositories;
using Canvas.Application.Board.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Board.Exceptions;

namespace Canvas.Application.Board.Handlers;

public class UpdateBoardHandler
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserRepository _userRepository;

    public UpdateBoardHandler(IBoardRepository boardRepository, IUserRepository userRepository)
    {
        _boardRepository = boardRepository;
        _userRepository = userRepository;
    }

    public async Task<Domain.Entities.Board> HandleAsync(UpdateBoardCommand updateBoardCommand)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(updateBoardCommand.userId);
        if (existingUser is null) throw new UserNotFoundException(updateBoardCommand.userId);

        var board = await _boardRepository.GetBoardByIdAsync(updateBoardCommand.boardId);
        if (board is null) throw new BoardNotFoundException(updateBoardCommand.boardId);

        if (board.UserId != updateBoardCommand.userId) throw new BoardNotOwnedByUserException(updateBoardCommand.boardId);

        board.UpdateBoard(updateBoardCommand.title, updateBoardCommand.description);

        await _boardRepository.UpdateBoardAsync(board);

        return board;
    }
}
using Canvas.Application.Board.Commands;
using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Board.Exceptions;
using Canvas.Application.Repositories;

namespace Canvas.Application.Board.Handlers;

public class DeleteBoardHandler
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserRepository _userRepository;

    public DeleteBoardHandler(IBoardRepository boardRepository, IUserRepository userRepository)
    {
        _boardRepository = boardRepository;
        _userRepository = userRepository;
    }

    public async Task<Domain.Entities.Board> HandleAsync(DeleteBoardCommand deleteBoardCommand)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(deleteBoardCommand.userId);
        if (existingUser is null) throw new UserNotFoundException(deleteBoardCommand.userId);

        var board = await _boardRepository.GetBoardByIdAsync(deleteBoardCommand.boardId);
        if (board is null) throw new BoardNotFoundException(deleteBoardCommand.boardId);

        if (board.UserId != deleteBoardCommand.userId) throw new BoardNotOwnedByUserException(deleteBoardCommand.boardId);

        await _boardRepository.DeleteBoardAsync(deleteBoardCommand.boardId);

        return board;
    }
}
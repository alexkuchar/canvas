using Canvas.Application.Auth.Exceptions;
using Canvas.Application.Board.Commands;
using Canvas.Application.Repositories;
using Canvas.Domain.Entities;

namespace Canvas.Application.Board.Handlers;

public class CreateBoardHandler
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUserRepository _userRepository;

    public CreateBoardHandler(IUserRepository userRepository, IBoardRepository boardRepository)
    {
        _userRepository = userRepository;
        _boardRepository = boardRepository;
    }

    public async Task<Domain.Entities.Board> HandleAsync(CreateBoardCommand createBoardCommand)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(createBoardCommand.userId);
        if (existingUser is null) throw new UserNotFoundException(createBoardCommand.userId);

        var board = new Domain.Entities.Board(
            title: createBoardCommand.title,
            description: createBoardCommand.description,
            user: existingUser,
            userId: createBoardCommand.userId
        );

        await _boardRepository.AddBoardAsync(board);

        return board;
    }
}
using System.Security.Claims;
using Canvas.Api.DTOs;
using Canvas.Application.Board.Commands;
using Canvas.Application.Board.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Canvas.Api.Controllers;

[ApiController]
[Route("api/boards")]
[Authorize]
public class BoardController : ControllerBase
{
    private readonly CreateBoardHandler _createBoardHandler;
    private readonly DeleteBoardHandler _deleteBoardHandler;
    private readonly GetBoardHandler _getBoardHandler;
    private readonly UpdateBoardHandler _updateBoardHandler;
    private readonly GetBoardsHandler _getBoardsHandler;

    public BoardController(CreateBoardHandler createBoardHandler, DeleteBoardHandler deleteBoardHandler, GetBoardHandler getBoardHandler, UpdateBoardHandler updateBoardHandler, GetBoardsHandler getBoardsHandler)
    {
        _createBoardHandler = createBoardHandler;
        _deleteBoardHandler = deleteBoardHandler;
        _getBoardHandler = getBoardHandler;
        _updateBoardHandler = updateBoardHandler;
        _getBoardsHandler = getBoardsHandler;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateBoardRequestDto createBoardRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

        var command = new CreateBoardCommand(
            userId: userId,
            title: createBoardRequestDto.Title,
            description: createBoardRequestDto.Description
        );

        var board = await _createBoardHandler.HandleAsync(command);

        return CreatedAtAction(nameof(Create), new CreateBoardResponseDto(
            Id: board.Id,
            Title: board.Title,
            Description: board.Description
        ));
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

        var command = new GetBoardCommand(
            userId: userId,
            boardId: id
        );

        var board = await _getBoardHandler.HandleAsync(command);

        return Ok(new GetBoardResponseDto(
            Id: board.Id,
            Title: board.Title,
            Description: board.Description
        ));
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBoardRequestDto updateBoardRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

        var command = new UpdateBoardCommand(
            userId: userId,
            boardId: id,
            title: updateBoardRequestDto.Title,
            description: updateBoardRequestDto.Description
        );

        var board = await _updateBoardHandler.HandleAsync(command);

        return Ok(new UpdateBoardResponseDto(
            Id: board.Id,
            Title: board.Title,
            Description: board.Description
        ));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

        var command = new DeleteBoardCommand(
            userId: userId,
            boardId: id
        );

        var board = await _deleteBoardHandler.HandleAsync(command);

        return Ok(new DeleteBoardResponseDto(
            Id: board.Id
        ));
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAll()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

        var command = new GetBoardsCommand(
            userId: userId
        );

        var boards = await _getBoardsHandler.HandleAsync(command);

        return Ok(boards.Select(b => new GetBoardResponseDto(
            Id: b.Id,
            Title: b.Title,
            Description: b.Description
        )));
    }
}
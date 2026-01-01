using System.ComponentModel.DataAnnotations;

namespace Canvas.Api.DTOs;

public record CreateBoardRequestDto(
    [Required]
    string Title,
    string? Description
);
namespace Canvas.Api.DTOs;
using System.ComponentModel.DataAnnotations;

public record UpdateBoardRequestDto(
    [Required]
    string Title,
    string? Description = null
);
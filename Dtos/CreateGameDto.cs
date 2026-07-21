using System.ComponentModel.DataAnnotations;
namespace GameStore.Api.Dtos;



public record class CreateGameDto(
 [Required][StringLength(100, MinimumLength = 3)] string Name,
 [Required][StringLength(100, MinimumLength = 3)] string Genre,
 [Required][Range(0.01, 1000.00)] decimal Price,
 DateOnly ReleaseDate
);
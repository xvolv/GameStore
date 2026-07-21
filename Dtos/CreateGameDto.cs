using System.ComponentModel.DataAnnotations;
namespace GameStore.Api.Dtos;



public record class CreateGameDto(
 [Required(ErrorMessage = "Name is required and must be between 3 and 100 characters long.")] [StringLength(100, MinimumLength = 3)] string Name,
 [Required(ErrorMessage = "Genre is required and must be between 3 and 100 characters long.")] [StringLength(100, MinimumLength = 3)] string Genre,
 [Required(ErrorMessage = "Price is required and must be a positive value.")] [Range(0.01, 1000.00)] decimal Price,
 DateOnly ReleaseDate
);

namespace GameStore.Api.Endpoints;

using GameStore.Api.Dtos;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;


public static class GamesEndPoints

{
    const string GetGameEndPointName = "GetGame";
    private static readonly List<GameDto> games = [

    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
          new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
          new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99m, new DateOnly(2018, 10, 26)),
        ];



    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games").WithTags("Games");

        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        }).WithParameterValidation();
        // Get a game by ID
        group.MapGet("/{id:int}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        }).WithName(GetGameEndPointName);
        // Create a new game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            game.Genre = dbContext.Genres.Find(newGame.GenreId);
       


            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game.ToDto());





        }).WithParameterValidation();
        //update a game by ID
        group.MapPut("/{id:int}", (int id, UpdateGameDto updatedGame) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            // find index
            var index = games.IndexOf(game);
            // update the game
            games[index] = game with
            {
                Name = updatedGame.Name,
                Genre = updatedGame.Genre,
                Price = updatedGame.Price,
                ReleaseDate = updatedGame.ReleaseDate
            };
            return Results.NoContent();
        }).WithParameterValidation();
        // delete a game by ID
        group.MapDelete("/{id:int}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        }).WithParameterValidation();

        return group;
    }

}

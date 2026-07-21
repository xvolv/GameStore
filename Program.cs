using GameStore.Api.Endpoints;
using GameStore.Api.Data;
using GameStore.Api.Entities;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GameDb");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

    if (!db.Genres.Any())
    {
        db.Genres.AddRange(
            new Genre { Name = "Action" },
            new Genre { Name = "Adventure" },
            new Genre { Name = "RPG" },
            new Genre { Name = "Sports" },
            new Genre { Name = "Racing" }
        );

        db.SaveChanges();
    }
}


app.MapGamesEndpoints();



app.MigrateDb();

app.Run();

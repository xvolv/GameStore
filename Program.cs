using GameStore.Api.Endpoints;
using GameStore.Api.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GameDb") ?? throw new InvalidOperationException("Connection string 'GameDb' not found.");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();


app.MapGamesEndpoints();



app.MigrateDb();

app.Run();

using GameStore.Api.Endpoints;
var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();
//call the endpoints method to map the endpoints
app.MapGamesEndpoints();

app.Run();

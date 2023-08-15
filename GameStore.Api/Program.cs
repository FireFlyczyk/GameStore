using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGamesStoreAuthorization();

var app = builder.Build(); 

await app.Services.InitializeDbContextAsync();
app.UseHttpLogging();

app.MapGamesEndpoints();
app.Run();



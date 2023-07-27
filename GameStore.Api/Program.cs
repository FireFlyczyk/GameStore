using GameStore.Api.Entities;

List<Game> games = new()
{
    new Game()
    {
        Id = 1,
        Name = "Witcher 3",
        Genre = "RPG",
        Price = 40,
        ReleaseDate = new DateTime(2015, 1, 1),
        ImageUrl = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 30),
        ImageUrl = "https://placehold.co/100 "
    },
    new Game()
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageUrl = "https://placehold.co/100 "
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var group = app.MapGroup("/games")
            .WithParameterValidation();

group.MapGet("/", () => games);

group.MapGet("/{id}", (int id) =>
{
    Game? game = games.FirstOrDefault(g => g.Id == id);
    return game == null ? Results.NotFound() : Results.Ok(games.FirstOrDefault(g => g.Id == id));
});
group.MapPost("/", (Game game) =>
{
    games.Add(game);
    return Results.Created($"/games/{game.Id}", game);
});
group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.FirstOrDefault(g => g.Id == id);
    if (game == null) return Results.NotFound();
    games.Remove(game);
    return Results.NoContent();
});

group.MapPut("/{id}", (int id, Game game) =>
{
    Game? existingGame = games.FirstOrDefault(g => g.Id == id);
    if (existingGame == null) return Results.NotFound();
    existingGame.Name = game.Name;
    existingGame.Genre = game.Genre;
    existingGame.Price = game.Price;
    existingGame.ReleaseDate = game.ReleaseDate;
    existingGame.ImageUrl = game.ImageUrl;
    return Results.NoContent();
});

app.Run();



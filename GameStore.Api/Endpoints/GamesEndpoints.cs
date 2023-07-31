using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {

        public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
        {

            var group = routes.MapGroup("/games")
                  .WithParameterValidation();
            group.MapGet("/", (IGamesRepository repository) => repository.GetAll());

            group.MapGet("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? game = repository.Get(id);
                return game == null ? Results.NotFound() : Results.Ok(game);
            });
            group.MapPost("/", (IGamesRepository repository, Game game) =>
            {
                repository.Create(game);
                return Results.Created($"/games/{game.Id}", game);
            });
            group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? game = repository.Get(id);
                if (game == null) return Results.NotFound();
                repository.Delete(id);
                return Results.NoContent();
            });

            group.MapPut("/{id}", (IGamesRepository repository, int id, Game game) =>
            {
                Game? existingGame = repository.Get(id);
                if (existingGame == null) return Results.NotFound();
                existingGame.Name = game.Name;
                existingGame.Genre = game.Genre;
                existingGame.Price = game.Price;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.ImageUrl = game.ImageUrl;

                repository.Update(existingGame);
                return Results.NoContent();
            });

            return group;
        }
    }
}
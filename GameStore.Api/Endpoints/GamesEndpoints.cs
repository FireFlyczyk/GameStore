using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Dtos;
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
            group.MapGet("/", (IGamesRepository repository) =>
            repository.GetAll().Select(game => game.AsDto()));

            group.MapGet("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? game = repository.Get(id);
                return game == null ? Results.NotFound() : Results.Ok(game.AsDto());
            });
            group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };
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

            group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto updateGameDto) =>
            {
                Game? existingGame = repository.Get(id);
                if (existingGame == null) return Results.NotFound();
                existingGame.Name = updateGameDto.Name;
                existingGame.Genre = updateGameDto.Genre;
                existingGame.Price = updateGameDto.Price;
                existingGame.ReleaseDate = updateGameDto.ReleaseDate;
                existingGame.ImageUri = updateGameDto.ImageUri;

                repository.Update(existingGame);
                return Results.NoContent();
            });

            return group;
        }
    }
}
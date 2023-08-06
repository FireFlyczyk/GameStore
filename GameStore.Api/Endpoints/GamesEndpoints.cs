using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Authorization;
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

            group.MapGet("/", async (IGamesRepository repository) =>
             (await repository.GetAllAsync()).Select(game => game.AsDto()));

            
            group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? game = await repository.GetAsync(id);
                return game == null ? Results.NotFound() : Results.Ok(game.AsDto());
            })
            .RequireAuthorization(Policies.ReadAccess);

            group.MapPost("/", async (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };
                await repository.CreateAsync(game);
                return Results.Created($"/games/{game.Id}", game);
            })
            .RequireAuthorization(Policies.WriteAccess);

            group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? game =  await repository.GetAsync(id);
                if (game == null) return Results.NotFound();
                await repository.DeleteAsync(id);
                return Results.NoContent();
            })
            .RequireAuthorization(Policies.WriteAccess);

            group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto updateGameDto) =>
            {
                Game? existingGame = await repository.GetAsync(id);
                if (existingGame == null) return Results.NotFound();
                existingGame.Name = updateGameDto.Name;
                existingGame.Genre = updateGameDto.Genre;
                existingGame.Price = updateGameDto.Price;
                existingGame.ReleaseDate = updateGameDto.ReleaseDate;
                existingGame.ImageUri = updateGameDto.ImageUri;

                await repository.UpdateAsync(existingGame);
                return Results.NoContent();
            })
            .RequireAuthorization(Policies.WriteAccess);

            return group;

        }
    }
}
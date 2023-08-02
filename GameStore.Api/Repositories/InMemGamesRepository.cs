using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Entities;

namespace GameStore.Api.Repositories
{
    public class InMemGamesRepository : IGamesRepository
    {
        private readonly List<Game> games = new()
    {
     new Game()
    {
        Id = 1,
        Name = "Witcher 3",
        Genre = "RPG",
        Price = 40,
        ReleaseDate = new DateTime(2015, 1, 1),
        ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010, 9, 30),
        ImageUri = "https://placehold.co/100 "
    },
    new Game()
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageUri = "https://placehold.co/100 "
    }};

        public IEnumerable<Game> GetAll()
        {
            return games;
        }
        public Game Get(int id)
        {
            return games.FirstOrDefault(g => g.Id == id);
        }
        public void Create(Game game)
        {
            games.Add(game);
        }
        public void Update(Game updatedGame)
        {
            var index = games.FindIndex(g => g.Id == updatedGame.Id);
            games[index] = updatedGame;

        }
        public void Delete(int id)
        {
            var index = games.FindIndex(g => g.Id == id);
            games.RemoveAt(index);
        }
    }


}
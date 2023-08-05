using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories
{
    public class EntityFrameworkGamesRepository : IGamesRepository
    {
        private readonly GameStoreContext dbContext;
        public EntityFrameworkGamesRepository(GameStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(Game game)
        {
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
          await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
        }

        public async Task< Game> GetAsync(int id)
        {
            return await dbContext.Games.FindAsync(id);
        }

        public async Task< IEnumerable<Game>> GetAllAsync()
        {
            return await dbContext.Games.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Game updatedGame)
        {
            dbContext.Games.Update(updatedGame);
            await dbContext.SaveChangesAsync();
        }
    }
}
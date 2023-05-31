using GameLogicService.DataContext;
using GameLogicService.Migrations;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLogicService.Repositories.Entity
{
    public class GameAccountAuthRepository : BaseRepository<GameAccountAuth>, IGameAccountAuthRepository
    {

        public GameAccountAuthRepository(DatabaseContext db)
            : base(db)
        {

        }
        public async Task<GameAccountAuth?> GetByAccountIdAsync(int accountId)
        {
            return await _db.GameAccountAuth
                .Where(gaa=>gaa.AccountId== accountId)
                .FirstOrDefaultAsync();
        }
    }
}

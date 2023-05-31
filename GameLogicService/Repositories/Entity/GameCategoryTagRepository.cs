using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLogicService.Repositories.Entity
{
    public class GameCategoryTagRepository : BaseRepository<GameCategoryTag>, IGameCategoryTagRepository
    {
        public GameCategoryTagRepository(DatabaseContext db)
            : base(db)
        {

        }

        public async Task<GameCategoryTag> GetByCategoryId(int categoryId)
        {
            return await _db.GameCategoryTag
                .Where(gct => gct.GameCategoryId == categoryId)
                .FirstOrDefaultAsync();
        }
    }
}

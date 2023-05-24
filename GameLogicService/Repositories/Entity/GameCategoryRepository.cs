using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLogicService.Repositories.Entity
{
    public class GameCategoryRepository : BaseRepository<GameCategory>, IGameCategoryRepository
    {
        public GameCategoryRepository(DatabaseContext db)
            : base(db)
        {

        }

        public async Task<int> GetCategoryIdByCategoryName(string categoryName)
        {
            return await _db.GameCategory
                .Where(c => c.CategoryName == categoryName)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
        }
    }
}

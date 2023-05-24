using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;

namespace GameLogicService.Repositories.Entity
{
    public class GameCategoryTagRepository : BaseRepository<GameCategoryTag>, IGameCategoryTagRepository
    {
        public GameCategoryTagRepository(DatabaseContext db)
            : base(db)
        {

        }
    }
}

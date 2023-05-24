using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;

namespace GameLogicService.Repositories.Entity
{
    public class PlayedQuizGameAccountRepository : BaseRepository<PlayedQuizGameAccount>, IPlayedQuizGameAccountRepository
    {
        public PlayedQuizGameAccountRepository(DatabaseContext db)
            : base(db)
        {

        }
    }
}

using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Repositories.Entity.Interfaces;

namespace GameLogicService.Repositories.Entity
{
    public class PlayedQuizRepository : BaseRepository<PlayedQuiz>, IPlayedQuizRepository
    {
        public PlayedQuizRepository(DatabaseContext db)
            : base(db)
        {

        }

    }
}

using GameLogicService.DataContext;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLogicService.Repositories.Entity
{
    public class GameOptionsRepository : BaseRepository<GameOptions>, IGameOptionsRepository
    {
        public GameOptionsRepository(DatabaseContext db)
            : base(db)
        {

        }

        public async Task<GameOptions?> GetGameOptionsByAccountEmailAddress(string accountEmailAddress)
        {
            IQueryable<GameOptions> query = (from gameOptions in _db.GameOptions
                                             join gameCombo in _db.GameCategoryGameAccount
                                             on gameOptions.Id equals gameCombo.GameCategoryId
                                             join gameAccount in _db.GameAccounts
                                             on gameCombo.GameAccountId equals gameAccount.Id
                                             where gameAccount.EmailAddress == accountEmailAddress
                                             select new GameOptions()
                                             {
                                                 Id = gameOptions.Id,
                                                 QuestionDifficultyId = gameOptions.QuestionDifficultyId,
                                                 QuestionCategoryId = gameOptions.QuestionCategoryId,
                                                 QuestionAmount = gameOptions.QuestionAmount,
                                                 QuestionDifficulty = (Difficulty)gameOptions.QuestionDifficultyId,
                                                 QuestionCategory = gameOptions.QuestionCategory,
                                             });

            return await query.FirstOrDefaultAsync();
        }
    }
}

using GameLogicService.Models.Entity;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A repository for game_account database table.
    /// </summary>
    public interface IGameAccountAuthRepository : IBaseRepository<GameAccountAuth>
    {
        /// <summary>
        /// Gets records by account Id.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>GameAccountAuthId EntityModel.</returns>
        Task<GameAccountAuth?> GetByAccountIdAsync(int accountId);
    }
}

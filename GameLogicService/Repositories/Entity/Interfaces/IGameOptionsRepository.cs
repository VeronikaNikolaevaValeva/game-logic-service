using GameLogicService.Models.Entity;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A repository for game_options database table.
    /// </summary>
    public interface IGameOptionsRepository : IBaseRepository<GameOptions>
    {
        /// <summary>
        /// Gets game options by accountUsername. 
        /// </summary>
        /// <param name="accountUsername"></param>
        /// <returns>GameOptions entity model</returns>
        Task<GameOptions?> GetGameOptionsByAccountEmailAddress(string accountEmailAddress);
    }   
}

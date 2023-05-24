using GameLogicService.Models.Entity;
using System.Security.Principal;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A repository for game_account database table.
    /// </summary>
    public interface IGameAccountRepository : IBaseRepository<GameAccount>
    {
        /// <summary>
        /// Gets records by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>GameAccount EntityModel.</returns>
        Task<GameAccount?> GetByEmailAsync(string email);

        /// <summary>
        /// Gets records by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>GameAccount EntityModel.</returns>
        Task<GameAccount?> GetByUsernameAsync(string username);

        /// <summary>
        /// Gets records by username and email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns>GameAccount EntityModel.</returns>
        Task<GameAccount?> GetByUsernameAndEmailAsync(string username, string email);
    }
}

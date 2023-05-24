using GameLogicService.Models.Entity;
using GameLogicService.Models.Response;

namespace GameLogicService.Services.Interfaces
{
    /// <summary>
    /// Service to perform operations on game accounts
    /// </summary>
    public interface IGameAccountService
    {
        /// <summary>
        /// A method for processing game account information. The system checks if the user is registered in the system.
        /// If not - a new game account is created and added to the database. 
        /// The gameAccountResponse param consists of username and e-mail address.
        /// </summary>
        /// <param name="gameAccountResponse"></param>
        /// <returns>A message with result info</returns>
        Task<ServiceProduct<string>> ProcessUser(GameAccountResponse gameAccountResponse);

    }
}

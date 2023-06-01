using GameLogicService.Models.Responses;

namespace GameLogicService.RestClientRequests.Interfaces
{
    /// <summary>
    /// A base client class is inherited by other client classes for shared dependencies and functions.
    /// </summary>
    public interface IAuthAPIRequests
    {
        /// <summary>
        /// Deletes a user on demand.
        /// </summary>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> DeleteAuthUserData(string authId);
    }
}

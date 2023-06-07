namespace GameLogicService.RestClientRequests.Interfaces
{
    /// <summary>
    /// A layer made for sending messages to Game Scoreboard Service.
    /// </summary>
    public interface IDeleteUserDataAPIRequests
    {
        /// <summary>
        /// Notigying the Game Scorboard Service whenever a user has to be deleted from the service.
        /// </summary>
        /// <returns>Result from the process.</returns>
        Task<string> DeleteUserData(string emailAddress);
    }
}

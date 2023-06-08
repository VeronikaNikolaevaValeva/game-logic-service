using GameLogicService.Models.Messaging;

namespace GameLogicService.Messaging.Interfaces
{
    /// <summary>
    /// Service to send messages to the game scoreboard service
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// Indicate to the game scoreboard service that a new account has been registered in the system
        /// </summary>
        /// <param name="accountData"></param>
        /// <returns>True - if data was send successfully, false otherwise</returns>
        void NewRegisteredUser(NewPlayerScoreEntity newPlayerScoreEntity);

        /// <summary>
        /// Indicate to the game scoreboard service that the score of a player has been updated.
        /// <param name="accountData"></param>
        /// <returns>True - if data was send successfully, false otherwise</returns>
        void UpdateUserScore(UpdateUserScore updateUserScore);

        /// <summary>
        /// Indicate to the game scoreboard service that the user under that email address has to be deleted
        /// <param name="emailAddress"></param>
        void DeleteUserData(string emailAddress);

        /// <summary>
        /// Check if any users have been successfully deleted. 
        /// <param name="accountData"></param>
        /// <returns>True - if data was send successfully, false otherwise</returns>
        bool DeletedUserData();
    }
}

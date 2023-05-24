using GameLogicService.Models.Entity;
using GameLogicService.Models.Response;
using GameLogicService.Models.Responses;

namespace GameLogicService.Services.Interfaces
{
    /// <summary>
    /// Service to perform operations on game options
    /// </summary>
    public interface IGameOptionsService
    {
        /// <summary>
        /// Gets a lisit of Game Quiz Questions.
        /// </summary>
        /// <param name="gameOptions"></param>
        /// <returns>LIst of GameQuestionResponse Entity Models</returns>
        Task<ServiceProduct<List<GameQuestionResponse>>> GetQuizGameQuestions(GameOptionsResponse gameOptions);

        /// <summary>
        /// Gets a lisit of Game Categories.
        /// </summary>
        /// <returns>List of GameCategoryExternalResponse entity models</returns>
        Task<ServiceProduct<List<GameCategoryExternalResponse>>> GetQuizGameCategories();

        /// <summary>
        /// Gets a lisit of Game Options by account username
        /// </summary>
        /// <param name="accountUsername"></param>
        /// <returns>GameOptionsResponse Entity Model</returns>
        Task<ServiceProduct<GameOptionsResponse>> GetGameOptionsByAccountEmailAddress(string accountEmailAddress);
    }
}

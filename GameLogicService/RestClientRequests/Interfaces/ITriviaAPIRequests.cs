using GameLogicService.Models.Entity;
using GameLogicService.Models.ExternalResponse;
using GameLogicService.Models.Responses;

namespace GameLogicService.RestClientRequests.Interfaces
{
    /// <summary>
    /// A layer made for outsourcing information from TriviaAPI. The calls are made to the Question & Answer Service.
    /// </summary>
    public interface ITriviaAPIRequests
    {
        /// <summary>
        /// Gets all available categories
        /// </summary>
        /// <returns>A list of GameCategoryExternalResponse entity models.</returns>
        Task<List<GameCategoryExternalResponse>> GetGameCategories();

        /// <summary>
        /// Gets questions by specific options.
        /// </summary>
        /// <returns>A list of GameCategoryExternalResponse entity models.</returns>
        Task<List<GameQuestionExternalResponse>> GetGameQuestions(GameOptionsExternalResponse gameOptions);
    }
}

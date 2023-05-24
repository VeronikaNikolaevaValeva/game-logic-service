using GameLogicService.Models.Response;

namespace GameLogicService.Services.Interfaces
{
    /// <summary>
    /// Service to perform operations on played quizes
    /// </summary>
    public interface IPlayedQuizService
    {
        /// <summary>
        /// Processes the filled quiz questions
        /// </summary>
        /// <returns>True if the process was a success, false otherwise</returns>
        Task<ServiceProduct<bool>> FilledQuizResponse(FilledQuizResponse quizResponse);
    }
}

using GameLogicService.Models.Enum;
using GameLogicService.Models.Response;
using GameLogicService.Services;
using GameLogicService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLogicService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class PlayedQuizController : ControllerBase
    {
        private readonly IPlayedQuizService _playedQUizService;

        public PlayedQuizController(IPlayedQuizService playedQuizService)
        {
            _playedQUizService = playedQuizService ?? throw new ArgumentNullException(nameof(playedQuizService));
        }

        /// <summary>
        /// Gets played quiz data and saves it
        /// </summary>
        /// <param name="quizResponse"></param>
        [HttpPost]
        [ActionName("GetFilledQuiz")]
        public async Task<ActionResult> GetQuizGameQuestions([FromBody] FilledQuizResponse quizResponse)
        {
            var result = await _playedQUizService.FilledQuizResponse(quizResponse);
            if (!result.Success && result.RejectionCode == RejectionCode.General)
                return Ok(result.RejectionReason);
            return Ok(result.Data);
        }
            
    }
}


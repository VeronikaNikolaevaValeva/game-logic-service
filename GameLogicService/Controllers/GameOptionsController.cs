using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Enum;
using GameLogicService.Models.Response;
using GameLogicService.Models.Responses;
using GameLogicService.Services;
using GameLogicService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLogicService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class GameOptionsController : ControllerBase
    {
        private readonly IGameOptionsService _gameOptionsService;

        public GameOptionsController(IGameOptionsService gameOptionsService)
        {
            _gameOptionsService = gameOptionsService ?? throw new ArgumentNullException(nameof(gameOptionsService));        }

        /// <summary>
        /// Get a list of game quiz categories
        /// </summary>
        /// <returns>List of GameCategoryExternalResponse Entity Models</returns>
        [HttpGet]
        [ActionName("GetQuizGameCategories")]
        public async Task<ActionResult<List<GameCategoryExternalResponse>>> GetQuizGameCategories()
        {
            var result = await _gameOptionsService.GetQuizGameCategories();
            if (!result.Success && result.RejectionCode == RejectionCode.General)
                return Ok(result.RejectionReason);
            return Ok(result.Data);
        }


        /// <summary>
        /// Get Quiz Options Entity model by accountId (logged account)
        /// </summary>
        /// <param name="accountEmailAddress"></param>
        /// <returns>GameOptionsResponse Entity Models</returns>
        [HttpGet]
        [ActionName("GetGameOptionsByAccountEmailAddress")]
        public async Task<ActionResult<GameOptionsResponse>> GetGameOptionsByAccountEmailAddress(string accountEmailAddress)
        {
            var result = await _gameOptionsService.GetGameOptionsByAccountEmailAddress(accountEmailAddress);
            if (!result.Success && result.RejectionCode == RejectionCode.General)
                return Ok(result.RejectionReason);
            return Ok(result.Data);
        }
        

        /// <summary>
        /// Gets a listof game question response entities.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List of GameQuestionResponse Entity Models</returns>
        [HttpPost]
        [ActionName("GetQuizGameQuestions")]
        public async Task<ActionResult<List<GameQuestionResponse>>> GetQuizGameQuestions([FromBody]GameOptionsResponse gameOptions)
        {
            var result = await _gameOptionsService.GetQuizGameQuestions(gameOptions);
            if (!result.Success && result.RejectionCode == RejectionCode.General)
                return Ok(result.RejectionReason);
            return Ok(result.Data);
        }
        
    }
}

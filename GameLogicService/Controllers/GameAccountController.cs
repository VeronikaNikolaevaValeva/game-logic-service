using GameLogicService.Models.Enum;
using GameLogicService.Models.Response;
using GameLogicService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLogicService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class GameAccountController : ControllerBase
    {
        private readonly IGameAccountService _gameAccountService;

        public GameAccountController(IGameAccountService gameAccountService)
        {
            _gameAccountService = gameAccountService ?? throw new ArgumentNullException(nameof(gameAccountService));
        }

        
        [HttpPost]
        [ActionName("ProcessUser")]
        public async Task<ActionResult<string>> ProcessUser([FromBody] GameAccountResponse gameAccountResponse)
        {
            Console.WriteLine(gameAccountResponse.ToString());
            var result = await _gameAccountService.ProcessUser(gameAccountResponse);
            if (!result.Success && result.RejectionCode == RejectionCode.General)
                return Ok(result.RejectionReason);
            return Ok(result.Data);
        }
    }
}

using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Models.ExternalResponse;
using GameLogicService.Models.Response;
using GameLogicService.Models.Responses;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.RestClientRequests.Interfaces;
using GameLogicService.Services.Interfaces;

namespace GameLogicService.Services
{
    public class GameOptionsService : BaseService, IGameOptionsService
    {
        private readonly IGameOptionsRepository _gameOptionsRepository;
        private readonly IGameAccountRepository _gameAccountRepository;
        private readonly IGameCategoryRepository _gameCategoryRepository;
        private readonly IGameCategoryTagRepository _gameCategoryTagRepository;
        private readonly IExternalAPIRequests _externalAPIRequests;

        public GameOptionsService(
            IGameOptionsRepository gameOptionsRepository,
            IGameAccountRepository gameAccountRepository,
            IExternalAPIRequests externalAPIRequests,
            IGameCategoryTagRepository gameCategoryTagRepository,
            IGameCategoryRepository gameCategoryRepository)
        {
            _gameOptionsRepository = gameOptionsRepository ?? throw new ArgumentNullException(nameof(gameOptionsRepository));
            _gameAccountRepository = gameAccountRepository ?? throw new ArgumentNullException(nameof(gameAccountRepository));
            _externalAPIRequests = externalAPIRequests ?? throw new ArgumentNullException(nameof(externalAPIRequests));
            _gameCategoryRepository = gameCategoryRepository ?? throw new ArgumentNullException(nameof(gameCategoryRepository));
            _gameCategoryTagRepository = gameCategoryTagRepository ?? throw new ArgumentNullException(nameof(gameCategoryTagRepository));
        }

        public async Task<ServiceProduct<List<GameCategoryExternalResponse>>> GetQuizGameCategories()
        {
            var result = await _externalAPIRequests.GetGameCategories();
            await AddCategoriesToDB(result);
            return result ?? new List<GameCategoryExternalResponse>();
        }

        private async Task AddCategoriesToDB(List<GameCategoryExternalResponse> gameCategories)
        {
            if (gameCategories.Any() == false || gameCategories is null) return;
            var existingGameCategories = await _gameCategoryRepository.GetAllAsync();
            foreach (var gc in gameCategories)
            {
                if (!existingGameCategories.Where(egc => egc.CategoryName == gc.CategoryName).Any())
                {
                    var result = await _gameCategoryRepository.AddAsync(new GameCategory() { CategoryName = gc.CategoryName });
                    if (result is not null)
                        foreach (var tag in gc.CategoryTags)
                        {
                            var tagResult = await _gameCategoryTagRepository.AddAsync(new GameCategoryTag() { GameCategoryId = result.Id, GameCategoryTagName = tag });
                            if (tagResult is null) break;
                        }
                }
            }
        }

        public async Task<ServiceProduct<GameOptionsResponse>> GetGameOptionsByAccountEmailAddress(string accountEmailAddress)
        {
            if (string.IsNullOrEmpty(accountEmailAddress)) return Reject<GameOptionsResponse>(RejectionCode.General, "The account username was null");
            var existingAccount = await _gameAccountRepository.GetByEmailAsync(accountEmailAddress);
            if(existingAccount is null) return Reject<GameOptionsResponse>(RejectionCode.General, $"No account was found with the follwoing account email address: {accountEmailAddress}");
            var result = await _gameOptionsRepository.GetGameOptionsByAccountEmailAddress(accountEmailAddress) ?? new GameOptions();
            if (result is null) return Reject<GameOptionsResponse>(RejectionCode.General, $"Something went wrong with trying to get the game options for the following account id: {existingAccount.Id}");
            var gameCategory = result.QuestionCategoryId != null ? await GetGameCategoryById((int)result.QuestionCategoryId) : null;
            return new GameOptionsResponse()
            {
                AccountId= existingAccount.Id,
                Amount = result.QuestionAmount ?? 10,
                Difficulty = result.QuestionDifficulty.ToString() ?? Difficulty.easy.ToString(),
                Category = gameCategory != null ? gameCategory.CategoryName.ToString() : null
            };
        }

        private async Task<GameCategory?> GetGameCategoryById(int categoryId) => await _gameCategoryRepository.GetByIdAsync(categoryId);

        public async Task<ServiceProduct<List<GameQuestionResponse>>> GetQuizGameQuestions(GameOptionsResponse gameOptions)
        {
            if(String.IsNullOrEmpty(gameOptions.Category)) return new List<GameQuestionResponse>();
            var categoryId = await _gameCategoryRepository.GetCategoryIdByCategoryName(gameOptions.Category);
            var categoryTag = await _gameCategoryTagRepository.GetByCategoryId(categoryId);
            if(categoryTag is null) return new List<GameQuestionResponse>();
            var result = await _externalAPIRequests.GetGameQuestions(new GameOptionsExternalResponse()
            {
                Category = categoryTag.GameCategoryTagName,
                Difficulty = gameOptions.Difficulty,
                Amount = gameOptions.Amount
            });
            var gameResults = new List<GameQuestionResponse>();
            result.ForEach(q => gameResults.Add(new GameQuestionResponse() 
            { 
                QuestionId = q.Id,
                CorrectAnswer = q.CorrectAnswer,
                IncorrectAnswers = q.IncorrectAnswers,
                Question = q.Question
            }));
            return gameResults ?? new List<GameQuestionResponse>();
        }
    }
}

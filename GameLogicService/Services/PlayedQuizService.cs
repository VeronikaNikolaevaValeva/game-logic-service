using GameLogicService.Messaging.Interfaces;
using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using GameLogicService.Models.Messaging;
using GameLogicService.Models.Response;
using GameLogicService.Repositories.Entity;
using GameLogicService.Repositories.Entity.Interfaces;
using GameLogicService.RestClientRequests.Interfaces;
using GameLogicService.Services.Interfaces;

namespace GameLogicService.Services
{
    public class PlayedQuizService : BaseService, IPlayedQuizService
    {
        private readonly IGameAccountRepository _gameAccountRepository;
        private readonly IGameCategoryRepository _gameCategoryRepository;
        private readonly IPlayedQuizRepository _playedQuizRepository;
        private readonly IPlayedQuizGameAccountRepository _playedQuizGameAccountRepository;
        private readonly IMessageSender _messageSender;

        public PlayedQuizService(
            IGameAccountRepository gameAccountRepository,
            IGameCategoryRepository gameCategoryRepository,
            IPlayedQuizRepository playedQuizRepository,
            IPlayedQuizGameAccountRepository playedQuizGameAccountRepository,
            IMessageSender messageSender)
        {
            _gameAccountRepository = gameAccountRepository ?? throw new ArgumentNullException(nameof(gameAccountRepository));
            _gameCategoryRepository = gameCategoryRepository ?? throw new ArgumentNullException(nameof(gameCategoryRepository));
            _playedQuizRepository = playedQuizRepository ?? throw new ArgumentNullException(nameof(playedQuizRepository));
            _playedQuizGameAccountRepository = playedQuizGameAccountRepository ?? throw new ArgumentNullException(nameof(playedQuizGameAccountRepository));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        public async Task<ServiceProduct<bool>> FilledQuizResponse(FilledQuizResponse quizResponse)
        {
            if (quizResponse == null) return Reject<bool>(RejectionCode.General, "Empty data");
            var existingAccount = await _gameAccountRepository.GetByEmailAsync(quizResponse.accountEmailAddress);
            if (existingAccount is null) return Reject<bool>(RejectionCode.General, "The user was not found");
            (int, int) answerData = this.GetNumberOfCorrectAndIncorrectAnswers(quizResponse.correctAnswers, quizResponse.givenAnswers);
            var categoryId = await _gameCategoryRepository.GetCategoryIdByCategoryName(quizResponse.category);
            if (categoryId == null) return Reject<bool>(RejectionCode.General, "Could not find category");
            var resultQuiz = await _playedQuizRepository.AddAsync(new PlayedQuiz()
            {
                NumberOfCorrectAnswers = answerData.Item1,
                NumberOfIncorrectAnswers = answerData.Item2,
                NumberOfQuestions = quizResponse.correctAnswers.Count,
                GameCategoryId = categoryId
            });
            if (resultQuiz is null) return Reject<bool>(RejectionCode.General, "Quiz data could not be saved");

            var resultQuizAccount = await _playedQuizGameAccountRepository.AddAsync(new PlayedQuizGameAccount()
            {
                GameAccountId = (int)existingAccount.Id,
                PlayedQuizId = resultQuiz.Id
            });
            if (resultQuizAccount is null) return Reject<bool>(RejectionCode.General, "Quiz data could not be saved");
            SendUpdateScoreToScoreService(resultQuiz, existingAccount);
            return true;
        }

        private (int, int) GetNumberOfCorrectAndIncorrectAnswers(List<List<string>?>? correctAnswers, List<List<string>?>? givenAnswers)
        {
            int correctAnswrsCount = 0;
            int incorrectAnswerCount = 0;
            foreach (var correctAnswer in correctAnswers)
            {
                foreach (var givenAnswer in givenAnswers)
                {
                    if (correctAnswer[0] == givenAnswer[0])
                    {
                        if (correctAnswer[1] == givenAnswer[1])
                        {
                            correctAnswrsCount++;
                            continue;
                        }
                        incorrectAnswerCount++;
                    }
                }
            }
            return (correctAnswrsCount, incorrectAnswerCount);
        }

        private void SendUpdateScoreToScoreService(PlayedQuiz quiz, GameAccount existingAccount)
        {
            _messageSender.UpdateUserScore(new UpdateUserScore()
            {
                EmailAddress = existingAccount.EmailAddress,
                AmountAnsweredQuestions = quiz.NumberOfCorrectAnswers + quiz.NumberOfIncorrectAnswers,
                AmountOfPlayedGames = 1,
                CorrectAnswerCount = quiz.NumberOfCorrectAnswers,
                IncorrectAnswerCount = quiz.NumberOfIncorrectAnswers,
                NonAnsweredCount= quiz.NumberOfQuestions - (quiz.NumberOfCorrectAnswers + quiz.NumberOfIncorrectAnswers),
            });
        }
    }
}

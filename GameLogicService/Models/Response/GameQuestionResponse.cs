using GameLogicService.Models.Enum;

namespace GameLogicService.Models.Response
{
    public class GameQuestionResponse
    {
        public string? QuestionId { get; set; }
        public string? CorrectAnswer { get; set; }
        public string[]? IncorrectAnswers { get; set; }
        public string? Question { get; set; }
    }
}

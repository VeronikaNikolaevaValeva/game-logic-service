using GameLogicService.Models.Enum;

namespace GameLogicService.Models.Entity
{
    public class GameQuestion : IBaseModel
    {
        public int Id { get; set; }
        public GameCategory? GameCategory { get; set; }
        public string? QuestionId { get; set; }
        public string? CorrectAnswer { get; set; }
        public string[]? IncorrectAnswers { get; set; }
        public string? Question { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}

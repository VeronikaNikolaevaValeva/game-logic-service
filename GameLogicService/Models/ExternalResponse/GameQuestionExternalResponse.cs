namespace GameLogicService.Models.ExternalResponse
{
    public class GameQuestionExternalResponse
    {
        public string? Category { get; set; }
        public string? Id { get; set; }
        public string? CorrectAnswer { get; set; }
        public string[]? IncorrectAnswers { get; set; }
        public string? Question { get; set; }
        public string[]? Tags { get; set; }
        public string? Type { get; set; }
        public string? Difficulty { get; set; }
        public string[]? Regions { get; set; }
        public bool? IsNiche { get; set; }
    }
    public class GameQuizResultsExternalResponse
    {
        public GameQuestionExternalResponse[]? Results { get; set; }
    }
}

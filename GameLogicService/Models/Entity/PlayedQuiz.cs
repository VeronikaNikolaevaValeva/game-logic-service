namespace GameLogicService.Models.Entity
{
    public class PlayedQuiz : IBaseModel
    {
        public int Id { get; set; }
        public int? NumberOfQuestions { get; set; }
        public int? NumberOfCorrectAnswers { get; set; }
        public int? NumberOfIncorrectAnswers { get; set; }
        public int? GameCategoryId { get; set; }
    }
}

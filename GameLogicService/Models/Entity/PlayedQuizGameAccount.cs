namespace GameLogicService.Models.Entity
{
    public class PlayedQuizGameAccount : IBaseModel
    {
        public int Id { get; set; }
        public int GameAccountId { get; set; }  
        public int PlayedQuizId { get; set; }
    }
}

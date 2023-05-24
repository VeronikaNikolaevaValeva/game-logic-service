namespace GameLogicService.Models.Entity
{
    public class GameCategoryGameAccount : IBaseModel
    {
        public int Id { get; set; }
        public int GameAccountId { get; set; }
        public int GameCategoryId { get; set; }
        public int GameOptionsId { get; set; }
    }
}

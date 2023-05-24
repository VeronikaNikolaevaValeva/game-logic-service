namespace GameLogicService.Models.Entity
{
    public class GameCategoryTag : IBaseModel
    {
        public int Id { get; set; }
        public int GameCategoryId { get; set; }
        public string? GameCategoryTagName { get; set; }
    }
}

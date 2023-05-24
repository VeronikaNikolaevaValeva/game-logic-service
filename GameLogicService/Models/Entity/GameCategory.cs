namespace GameLogicService.Models.Entity
{
    public class GameCategory : IBaseModel
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public List<GameCategoryTag>? CategoryTags { get; set; }
    }
}

using GameLogicService.Models.Entity;

namespace GameLogicService.Models.Responses
{
    public class GameCategoryExternalResponse
    {
        public string CategoryName { get; set; }
        public List<string> CategoryTags { get; set; }
    }
}

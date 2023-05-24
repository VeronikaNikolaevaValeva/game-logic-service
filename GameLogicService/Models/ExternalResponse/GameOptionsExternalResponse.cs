using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;

namespace GameLogicService.Models.ExternalResponse
{
    public class GameOptionsExternalResponse
    {
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? Amount { get; set; }
    }

}

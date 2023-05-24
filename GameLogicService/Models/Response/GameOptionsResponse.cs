using GameLogicService.Models.Entity;
using GameLogicService.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLogicService.Models.Response
{
    public class GameOptionsResponse
    {
        public int? AccountId { get; set; }
        public string? Category { get; set; }
        public string? Difficulty { get; set; }
        public int? Amount { get; set; }
        public bool? Preferences { get; set; }
    }
}

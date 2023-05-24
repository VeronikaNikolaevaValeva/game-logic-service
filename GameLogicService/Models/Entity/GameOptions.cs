using GameLogicService.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLogicService.Models.Entity
{
    public class GameOptions : IBaseModel
    {
        public int Id { get; set; } 
        public int? QuestionDifficultyId { get; set; } 
        public int? QuestionCategoryId { get; set; }
        public int? QuestionAmount { get; set; }
        public Difficulty? QuestionDifficulty { get; set; }
        public GameCategory? QuestionCategory{ get; set; }

        public GameOptions() 
        { 
            QuestionDifficulty = Difficulty.easy;
            QuestionCategory = new GameCategory();
        }
    }
}

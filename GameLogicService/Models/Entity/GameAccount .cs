using System.ComponentModel.DataAnnotations.Schema;

namespace GameLogicService.Models.Entity
{
    public class GameAccount : BaseEntityChangeTracker, IBaseModel
    {
        public int Id { get; set; }
        
        public string? Username { get; set; }

        //public DateTime? LastLogin { get; set; }

        public string? EmailAddress { get; set; }

    }
}

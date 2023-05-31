namespace GameLogicService.Models.Entity
{
    public class GameAccountAuth : IBaseModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AuthId { get; set; }
    }
}

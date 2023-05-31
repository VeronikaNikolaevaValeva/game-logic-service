using GameLogicService.Models.Entity;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A repository for game_category_tag database table.
    /// </summary>
    public interface IGameCategoryTagRepository : IBaseRepository<GameCategoryTag>
    {
        /// <summary>
        /// Gets game category tag by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>GameCategoryTag entity model</returns>
        Task<GameCategoryTag> GetByCategoryId(int categoryId);
    }
}

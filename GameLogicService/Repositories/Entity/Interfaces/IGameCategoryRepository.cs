using GameLogicService.Models.Entity;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A repository for game_category database table.
    /// </summary>
    public interface IGameCategoryRepository : IBaseRepository<GameCategory>
    {
        /// <summary>
        /// Gets the category id by category name
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Category id</returns>
        Task<int> GetCategoryIdByCategoryName(string categoryName);
    }
}

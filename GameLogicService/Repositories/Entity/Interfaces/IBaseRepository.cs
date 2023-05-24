using GameLogicService.Models;
using System.Linq.Expressions;

namespace GameLogicService.Repositories.Entity.Interfaces
{
    /// <summary>
    /// A base repository which is inherited by other repositories and has basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IBaseRepository<T> where T: class, IBaseModel
    {
        /// <summary>
        /// Gets a record by Id.
        /// 
        /// If item with the given id is already being tracked, then return item without querying the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Gets a record by Id.
        /// Also load related properties, if these are specified by includes.
        /// 
        /// Will always hit underlying DB connection
        /// </summary>
        /// <example>
        /// <code>
        ///     await _accountRoleRepository.GetByIdsAsync(ids, 
        ///         r => r.RolePropertyOne,
        ///         r => r.RolePropertyTwo,
        ///         r => r.RolePropertyThree);
        /// </code>
        /// </example>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns>An EntityModel.</returns>
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object?>>[] includes);

        /// <summary>
        /// Gets all records from the table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A set of EntityModels.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adds a new record to the table.
        /// WARNING: if save changes is set to false, then do the same for other operations and then at the end use method SaveChanges() to save changes from all operations.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Added EntityModel.</returns>
        /// <remarks>will only Add the toplevel entity, this excludes relational entities.</remarks>
        Task<T?> AddAsync(T entity);

        /// <summary>
        /// Updates a record in the table.
        /// WARNING: if save changes is set to false, then do the same for other operations and then at the end use method SaveChanges() to save changes from all operations.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Updated EntityModel.</returns>
        /// <remarks>will only update the entity's table records, this excludes navigation properties for this update the foreign key instead.</remarks>
        Task<T?> UpdateAsync(T entity);

        /// <summary>
        /// Finds a record by an Id and deletes it from the table.
        /// WARNING: if save changes is set to false, then do the same for other operations and then at the end use method SaveChanges() to save changes from all operations.
        /// </summary>
        /// <param name="id">S</param>
        /// <returns>True if a record was successfully deleted from the table and false if it failed.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Delete provided record from table, 
        /// avoids extra database roundtrip done by DelteAsync(id).
        /// WARNING: if save changes is set to false, then do the same for other operations and then at the end use method SaveChanges() to save changes from all operations.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if a record was successfully deleted from the table and false if it failed.</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Get an IEnumerable of entities from the database, where the Id was in the provided ids.
        /// 
        /// Order of the provided ids enumerable does not need to be honoured, and returned elements may therefore be in any order.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByIdsAsync(params int[] ids);

        /// <summary>
        /// Get an IEnumerable of entities from the database, where the Id was in the provided ids enumerable.
        /// Eager load any properties yielded by the includes methods
        /// 
        /// Order of the provided ids enumerable does not need to be honoured, and returned elements may therefore be in any order.
        /// 
        /// </summary>
        /// <example>
        /// <code>
        ///     await _gameAccountRepository.GetByIdsAsync(ids, 
        ///         r => r.Name,
        ///         r => r.EmailAddress);
        /// </code>
        /// </example>
        /// <param name="ids"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, params Expression<Func<T, object?>>[] includes);
    }
}

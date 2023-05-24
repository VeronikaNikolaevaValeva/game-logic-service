using GameLogicService.Models;
using GameLogicService.DataContext;
using GameLogicService.Repositories.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameLogicService.Repositories.Entity
{
    /// <inheritdoc />
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : class, IBaseModel
    {
        protected readonly DatabaseContext _db;

        //Compiled Queries
        private static readonly Func<DatabaseContext, IAsyncEnumerable<TEntity>> _getAllAsync =
            EF.CompileAsyncQuery((DatabaseContext context) =>
                context.Set<TEntity>());

        private static readonly Func<DatabaseContext, int[], IAsyncEnumerable<TEntity>> _findByIdsAsync =
            EF.CompileAsyncQuery((DatabaseContext context, int[] ids) =>
                context.Set<TEntity>().Where(x => ids.Contains(x.Id)));

        private static readonly Func<DatabaseContext, int, Task<TEntity?>> _findByIdAsync =
            EF.CompileAsyncQuery((DatabaseContext context, int id) =>
                context.Set<TEntity>().FirstOrDefault(x => x.Id == id));

        private static async Task<List<T>> ToListAsync<T>(IAsyncEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                results.Add(item);
            }

            return results;
        }

        protected BaseRepository(DatabaseContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await ToListAsync(_getAllAsync(_db));
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(params int[] ids)
        {
            return await ToListAsync(_findByIdsAsync(_db, ids));
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();
            foreach (var includeFunc in includes)
            {
                query = query.Include(includeFunc);
            }
            return await query.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _findByIdAsync(_db, id);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>();
            foreach (var includeFunc in includes)
            {
                query = query.Include(includeFunc);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> AddAsync(TEntity entity)
        {
            try
            {
                var dbEntry = _db.Entry(entity).State = EntityState.Added;
                await _db.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Any ConcurrencyException caught during updating records should be handled here (e.g. trying to update a deleted entry)
                return null;
            }
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var result = await _db.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                try
                {
                    _db.Set<TEntity>().Remove(result);
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Any ConcurrencyException caught during updating records should be handled here (e.g. trying to update a deleted entry)
                    return false;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                var dbEntry = _db.Entry(entity).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Any ConcurrencyException caught during updating records should be handled here (e.g. trying to update a deleted entry)
                return false;
            }
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            // Check if the entity being updated has a changetracker, if not ef will update it's whole row (this breaks data concurrency)
            if (entity is BaseEntityChangeTracker model)
            {
                var dbEntry = _db.Entry(entity);

                // Find all changed properties and update the IsModified state to tell ef that only those properties have to be updated
                foreach (var changedProp in model.GetChangedProperties())
                {
                    try
                    {
                        dbEntry.Property(changedProp).IsModified = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Catch an attempt to set a member that is not allowed to change, such as a primary key or navigation properties.
                    }
                }
            }
            else
            {
                // If entity does not have a changetracker then set the whole entity as modified,
                // this will update the whole data row and can cause data concurrency issues.
                _db.Entry(entity).State = EntityState.Modified;
            }
            // Update Database, if a ConcurrencyException occurs, return null
            try
            {
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

        }

    }
}

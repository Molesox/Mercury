using Mercury.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Mercury.Server.Data
{
    /// <summary>
    /// RepositoryEF is an implementation of IRepositoryGenericGet interface that uses Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository works with.</typeparam>
    /// <typeparam name="TDataContext">The type of the DbContext that this repository uses.</typeparam>
    public class RepositoryEF<TEntity, TDataContext> : IRepositoryGenericGet<TEntity>
        where TEntity : class
        where TDataContext : DbContext
    {
        protected readonly TDataContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryEF(TDataContext dataContext)
        {
            context = dataContext;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Deletes the specified entity from the repository.
        /// </summary>
        /// <param name="entityToDelete">Entity to delete.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public virtual async Task<bool> Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;
        }

        /// <summary>
        /// Deletes an entity in the repository using its ID.
        /// </summary>
        /// <param name="id">ID of the entity to delete.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
        public virtual async Task<bool> Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete is null) return false;
            return await Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Get method for retrieving entities.
        /// </summary>
        /// <param name="filter">A function to test each element for a condition; only elements that pass the test are included in the returned collection.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="includeProperties">Properties to be included in the query result.</param>
        /// <returns>An IEnumerable of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {//todo: handle nulls 
            try
            {
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = dbSet;

                // Apply the filter
                if (filter is not null) query = query.Where(filter);

                // Include the specified properties
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                // Sort
                if (orderBy is not null) return orderBy(query).ToList();
                else return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                // log exception here
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <returns>An IEnumerable of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            if (typeof(TEntity).Name.Contains("Search"))
            { 
                // Use reflection to check if a corresponding EntitySearch function exists
                var methodName = typeof(TEntity).Name;
                var searchFunction = typeof(TDataContext).GetMethod(methodName);
                if (searchFunction != null)
                {
                    // If it exists, invoke it with null parameters (or adjust as needed)
                    var result = searchFunction.Invoke(context, new object[] { });

                    // Ensure result is IQueryable<TEntity>
                    if (result is IQueryable<TEntity> queryableResult)
                    {
                        return await queryableResult.ToListAsync();
                    }
                }
            }
            return await context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity using its ID.
        /// </summary>
        /// <param name="id">ID of the entity to retrieve.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public virtual async Task<TEntity?> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        /// Inserts a new entity into the repository.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>The inserted entity.</returns>
        public virtual async Task<TEntity?> Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        /// <returns>The updated entity.</returns>
        public virtual async Task<TEntity?> Update(TEntity entityToUpdate)
        {
            var dbSet = context.Set<TEntity>();
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }
    }
}



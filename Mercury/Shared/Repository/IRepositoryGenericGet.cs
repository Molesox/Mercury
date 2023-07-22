using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Shared.Repository
{
    #region IRepositoryGenericGet interface
    /// <summary>
    /// The IRepositoryGenericGet interface provides the standard operations to be performed on a data repository for a given type,
    /// including a generic Get method.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that this repository works with.</typeparam>
    public interface IRepositoryGenericGet<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Methods

        /// <summary>
        /// Generic Get method for retrieving entities.
        /// </summary>
        /// <param name="filter">A function to test each element for a condition; only elements that pass the test are included in the returned collection.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="includeProperties">Properties to be included in the query result.</param>
        /// <returns>An IEnumerable of entities.</returns>
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
            
        #endregion
    }
    #endregion

}

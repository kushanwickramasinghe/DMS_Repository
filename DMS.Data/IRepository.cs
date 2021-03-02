using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Entity By Id.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        /// <returns>returns entity.</returns>
        /// 
        TEntity GetById(object id);

        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);

        /// <summary>
        /// Insert an Entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Created entity.</returns>
        /// 
        void Insert(TEntity item);

        /// <summary>
        /// Delete an Entity for the data store.
        /// </summary>
        /// <param name="entity">Entity to be delete <see cref="Model"/>.</param>
        /// 
        void Delete(TEntity item);

        /// <summary>
        /// Returns a <see cref="IQueryable"/> for querying the entity bound to this.
        /// </summary>
        /// <returns><see cref="IQueryable"/> for querying the entity bound to this.</returns>
        /// 
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns a <see cref="IQueryable"/> for querying the entity bound to this.
        /// </summary>
        /// <param name="predicate"><see cref="Expression{Func{T, bool}}"/> lambda expression to entityList.</param>
        /// <returns><see cref="IQueryable{T}"/> for querying the filtered results.</returns>
        /// 
        IQueryable<TEntity> LinqQuery(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Custom Select Sql Queries.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Void.</returns>
        /// 
        IQueryable<TEntity> GetSelected(string query);

        /// <summary>
        /// Update an Entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Void.</returns>
        /// 
        void Update(TEntity entity);


        /// <summary>
        /// Get the entities which match filter query, and loads related entities mentioned in the includes parameter array
        /// </summary>
        /// <param name="filter">Lambda expression to query</param>
        /// <param name="includes">Array of lambda expressions to explicitly load related child entities</param>
        /// <returns>Queryable collection of entities</returns>
        /// 
        IQueryable<TEntity> IncludeMultiple(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get all the entities for the TEntity with related entities mentioned in the includes parameter array
        /// </summary>
        /// <param name="includes">Array of lambda expressions to explicitly load related child entities</param>
        /// <returns>Queryable collection of entities</returns>
        /// 
        IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includes);
    }
}

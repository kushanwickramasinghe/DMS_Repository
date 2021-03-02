using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.EntityModel;

namespace DMS.Data
{
    using System.Data.Entity;

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;

        private DbContext Context { get; set; }

        //Set the Context when create the GenericRepository
        public GenericRepository(DbContext context)
        {
            this.Context = context;
            //Set DbSet of the DbContext
            this.dbSet = context.Set<TEntity>();

        }

        /// <summary>
        /// Retrieves an entity by its Primary key. This method should be used when the type of the primary key is not known or it is not a long.
        /// If the type of the primary key is a long, then using the method FindById will give better performance.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public TEntity GetById(object id)
        {
            return dbSet.Find(id);

        }

        public IEnumerable<TEntity> Search(Func<TEntity, bool> filter)
        {

            return dbSet.Where(filter);
        }

        /// <summary>
        /// insert an Entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Created entity.</returns>
        /// 
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Delete an Entity for the data store.
        /// </summary>
        /// <param name="entity">Entity to be delete <see cref="Model"/>.</param>
        /// 
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Update an Entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Void.</returns>
        /// 
        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = System.Data.EntityState.Modified;
        }

        /// <summary>
        /// Returns a <see cref="IQueryable"/> for querying the entity bound to this.
        /// </summary>
        /// <param name="predicate"><see cref="Expression{Func{T, bool}}"/> lambda expression to entityList.</param>
        /// <returns><see cref="IQueryable{T}"/> for querying the filtered results.</returns>
        /// 
        public IQueryable<TEntity> LinqQuery(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Where(filter);
        }

        /// <summary>
        /// Returns a <see cref="IQueryable"/> for querying the entity bound to this.
        /// </summary>
        /// <returns><see cref="IQueryable"/> for querying the entity bound to this.</returns>
        /// 
        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// Custom Select Sql Queries.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Void.</returns>
        /// 
        public IQueryable<TEntity> GetSelected(string query)
        {
            DbSet<TEntity> dbSet = Context.Set<TEntity>();
            IQueryable<TEntity> resultSet = dbSet.SqlQuery(query).AsQueryable();

            return resultSet;

        }


        /// <summary>
        /// Get the entities which match filter query, and loads related entities mentioned in the includes parameter array
        /// </summary>
        /// <param name="filter">Lambda expression to query</param>
        /// <param name="includes">Array of lambda expressions to explicitly load related child entities</param>
        /// <returns>Queryable collection of entities</returns>
        /// 
        public IQueryable<TEntity> IncludeMultiple(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> entityList = dbSet.Where(filter);

            if (includes != null)
            {
                entityList = includes.Aggregate(entityList, (current, include) => current.Include(include));
            }

            return entityList;
        }


        /// <summary>
        /// Get all the entities for the TEntity with related entities mentioned in the includes parameter array
        /// </summary>
        /// <param name="includes">Array of lambda expressions to explicitly load related child entities</param>
        /// <returns>Queryable collection of entities</returns>
        /// 
        public IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> entityList = dbSet;

            if (includes != null)
            {
                entityList = includes.Aggregate(entityList, (current, include) => current.Include(include));
            }

            return entityList;
        }
    }
}

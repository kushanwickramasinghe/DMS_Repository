using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Data;

namespace DMS.Repository
{
    public class UnitOfWork
    {
        private DMSEntities context;

        //Dictionary keeps list of repositories related to specfic entity
        private Dictionary<Type, object> customRepositoryList = new Dictionary<Type, object>();

        #region Public Properties

        /// <summary>
        /// Indicate if the object has been disposed
        /// </summary>
        public bool IsDisposed { get; set; }

        /// <summary>
        /// Gets or sets if the object is locked which prevents it from being disposed.
        /// </summary>
        public bool IsLockedForDisposing { get; set; }

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        public UnitOfWork()
        {
            //Initiate the DbContext
            context = new DMSEntities();
        }

        /// <summary>
        /// This method will return the Repository specific to particular entity 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>customRepositoryList as IRepository</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type t = typeof(TEntity);

            // Check if the generic repository has already been created for the particular entity
            if (!customRepositoryList.ContainsKey(typeof(TEntity)))
            {
                //Initiate the GenericRepository object specific to entity
                IRepository<TEntity> genericRepository = new GenericRepository<TEntity>(context);

                customRepositoryList.Add(typeof(TEntity), genericRepository);
            }
            //return the customRepositoryList as IRepository object
            return customRepositoryList[typeof(TEntity)] as IRepository<TEntity>;
        }


        /// <summary>
        /// Save the changes.
        /// </summary>
        /// 
        public void SaveChanges()
        {
            context.SaveChanges();

        }

        #region IDisposable Implementation

        private bool disposed = false;

        /// <summary>
        /// Dispose the Loaded Context
        /// </summary>
        /// 
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Disposing of managed code
                    context.Dispose();

                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            if (!IsLockedForDisposing)
            {
                IsDisposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}

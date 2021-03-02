using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Data;

namespace DMS.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get or set if the object has been disposed.
        /// </summary>
        bool IsDisposed { get; set; }

        /// <summary>
        /// Gets or sets if the object is locked which prevents it from being disposed.
        /// </summary>
        bool IsLockedForDisposing { get; set; }

        /// <summary>
        /// Save the changes.
        /// </summary>
        /// 
        void SaveChanges();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        dynamic GetRepository(Type type);
    }
}

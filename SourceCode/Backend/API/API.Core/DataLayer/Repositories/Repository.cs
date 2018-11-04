using System;
using API.Core.EntityLayer;

namespace API.Core.DataLayer.Contracts
{
    public class Repository
    {
        public Repository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected StoreDbContext DbContext { get; }

        public virtual void Add<TEntity>(TEntity entity) where TEntity : class
        {
            // Cast entity to IAuditEntity
            if (entity is IAuditEntity cast)
            {
                // Set creation date time
                cast.CreationDateTime = DateTime.Now;
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IAuditEntity cast)
            {
                // Set last update date time
                cast.LastUpdateDateTime = DateTime.Now;
            }

            DbContext.Set<TEntity>().Update(entity);
        }

        public virtual void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}

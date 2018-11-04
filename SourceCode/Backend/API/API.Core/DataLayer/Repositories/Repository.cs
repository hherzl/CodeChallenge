using System;
using System.Threading.Tasks;
using API.Core.EntityLayer;

namespace API.Core.DataLayer.Contracts
{
    public class Repository
    {
        public Repository(CodeChallengeDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected CodeChallengeDbContext DbContext { get; }

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

        public int CommitChanges()
            => DbContext.SaveChanges();

        public Task<int> CommitChangesAsync()
            => DbContext.SaveChangesAsync();
    }
}

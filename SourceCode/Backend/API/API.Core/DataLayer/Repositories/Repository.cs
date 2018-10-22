using System;
using System.Threading.Tasks;
using API.Core.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer.Contracts
{
    public class Repository
	{
		protected bool Disposed;
		protected CodeChallengeDbContext DbContext;

		public Repository(CodeChallengeDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public void Dispose()
		{
			if (!Disposed)
			{
				DbContext?.Dispose();
			
				Disposed = true;
			}
		}

		public virtual void Add<TEntity>(TEntity entity) where TEntity : class
		{
            // Cast entity to IAuditEntity
            if (entity is IAuditEntity cast)
            {
                if (!cast.CreationDateTime.HasValue)
                {
                    // Set creation date time
                    cast.CreationDateTime = DateTime.Now;
                }
            }

            // Get entry from Db context
            var entry = DbContext.Entry(entity);
			
			if (entry.State != EntityState.Detached)
			{
				// Set state for entity entry
				entry.State = EntityState.Added;
			}
			else
			{
				// Add entity to DbSet
				DbContext.Set<TEntity>().Add(entity);
			}
		}

		public virtual void Update<TEntity>(TEntity entity) where TEntity : class
		{
            if (entity is IAuditEntity cast)
            {
                if (!cast.LastUpdateDateTime.HasValue)
                {
                    // Set last update date time
                    cast.LastUpdateDateTime = DateTime.Now;
                }
            }

            // Get entity's entry
            var entry = DbContext.Entry(entity);
			
			if (entry.State == EntityState.Detached)
			{
				// Attach entity to DbSet
				DbContext.Set<TEntity>().Attach(entity);
			}
			
			entry.State = EntityState.Modified;
		}

		public virtual void Remove<TEntity>(TEntity entity) where TEntity : class
		{
			// Get entity's entry
			var entry = DbContext.Entry(entity);
			
			if (entry.State == EntityState.Deleted)
			{
				// Create set for entity
				var dbSet = DbContext.Set<TEntity>();
			
				// Attach and remove entity from DbSet
				dbSet.Attach(entity);
				dbSet.Remove(entity);
			}
			else
			{
				// Set state for entity to 'Deleted'
				entry.State = EntityState.Deleted;
			}
		}

		public int CommitChanges()
			=> DbContext.SaveChanges();

		public Task<int> CommitChangesAsync()
			=> DbContext.SaveChangesAsync();
	}
}

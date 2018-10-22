using System;
using System.Threading.Tasks;

namespace API.Core.DataLayer.Contracts
{
    public interface IRepository : IDisposable
	{
		void Add<TEntity>(TEntity entity) where TEntity : class;

		void Update<TEntity>(TEntity entity) where TEntity : class;

		void Remove<TEntity>(TEntity entity) where TEntity : class;

		int CommitChanges();

		Task<int> CommitChangesAsync();
	}
}

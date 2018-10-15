using System;
using System.Threading.Tasks;

namespace API.Core.DataLayer.Contracts
{
	public interface IRepository : IDisposable
	{
		void Add<TEntity>(TEntity entity) where TEntity : class;

		void Update<TEntity>(TEntity entity) where TEntity : class;

		void Remove<TEntity>(TEntity entity) where TEntity : class;

		Int32 CommitChanges();

		Task<Int32> CommitChangesAsync();
	}
}

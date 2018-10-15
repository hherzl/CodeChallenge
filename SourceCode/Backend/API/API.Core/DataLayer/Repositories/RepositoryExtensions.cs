using System;
using System.Linq;
using API.Core.DataLayer;
using API.Core.EntityLayer;

namespace API.Core.DataLayer.Repositories
{
	public static class RepositoryExtensions
	{
		public static IQueryable<TEntity> Paging<TEntity>(this CodeChallengeDbContext dbContext, Int32 pageSize = 0, Int32 pageNumber = 0) where TEntity : class
		{
			var query = dbContext.Set<TEntity>().AsQueryable();
			
			return pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
		}

		public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, Int32 pageSize = 0, Int32 pageNumber = 0) where TModel : class
			=> pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
	}
}

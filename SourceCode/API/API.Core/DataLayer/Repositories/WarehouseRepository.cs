using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Core.EntityLayer.Sales;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Repositories
{
	public class WarehouseRepository : Repository, IWarehouseRepository
	{
		public WarehouseRepository(CodeChallengeDbContext dbContext)
			: base(dbContext)
		{
		}

		public IQueryable<Product> GetProducts()
		{
			// Get query from DbSet
			var query = DbContext.Set<Product>().AsQueryable();
			
			return query;
		}

		public async Task<Product> GetProductAsync(Product entity)
			=> await DbContext.Set<Product>().FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);

		public async Task<Product> GetProductByProductNameAsync(Product entity)
			=> await DbContext.Set<Product>().FirstOrDefaultAsync(item => item.ProductName == entity.ProductName);

		public IQueryable<ProductPriceHistory> GetProductPriceHistories()
		{
			// Get query from DbSet
			var query = DbContext.Set<ProductPriceHistory>().AsQueryable();
			
			return query;
		}

		public async Task<ProductPriceHistory> GetProductPriceHistoryAsync(ProductPriceHistory entity)
			=> await DbContext.Set<ProductPriceHistory>().FirstOrDefaultAsync(item => item.ProductPriceHistoryID == entity.ProductPriceHistoryID);
	}
}

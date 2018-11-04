using System.Linq;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer.Repositories
{
    public class WarehouseRepository : Repository, IWarehouseRepository
	{
		public WarehouseRepository(StoreDbContext dbContext)
			: base(dbContext)
		{
		}

        public IQueryable<Product> GetProducts(string name = "")
        {
            // Get query from DbSet
            var query = DbContext.Set<Product>().AsQueryable();

            // Get only available products
            query = query.Where(item => item.Available == true);

            // Search by name
            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.ProductName.ToLower().Contains(name.ToLower()));

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

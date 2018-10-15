using System.Linq;
using System.Threading.Tasks;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Contracts
{
    public interface IWarehouseRepository : IRepository
	{
		IQueryable<Product> GetProducts(string name = "");

		Task<Product> GetProductAsync(Product entity);

		Task<Product> GetProductByProductNameAsync(Product entity);

		IQueryable<ProductPriceHistory> GetProductPriceHistories();

		Task<ProductPriceHistory> GetProductPriceHistoryAsync(ProductPriceHistory entity);
	}
}

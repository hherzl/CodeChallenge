using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Core.EntityLayer.Sales;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Contracts
{
	public interface IWarehouseRepository : IRepository
	{
		IQueryable<Product> GetProducts();

		Task<Product> GetProductAsync(Product entity);

		Task<Product> GetProductByProductNameAsync(Product entity);

		IQueryable<ProductPriceHistory> GetProductPriceHistories();

		Task<ProductPriceHistory> GetProductPriceHistoryAsync(ProductPriceHistory entity);
	}
}

using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.EntityLayer;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer
{
    public static class StoreDbContextExtensions
    {
        public static void Add<TEntity>(this StoreDbContext dbContext, TEntity entity) where TEntity : class
        {
            // Set creation date time
            if (entity is IAuditEntity cast)
                cast.CreationDateTime = DateTime.Now;

            dbContext.Set<TEntity>().Add(entity);
        }

        public static void Update<TEntity>(this StoreDbContext dbContext, TEntity entity) where TEntity : class
        {
            // Set last update date time
            if (entity is IAuditEntity cast)
                cast.LastUpdateDateTime = DateTime.Now;

            dbContext.Set<TEntity>().Update(entity);
        }

        public static IQueryable<Product> GetProducts(this StoreDbContext dbContext, string name = "")
        {
            // Get query from DbSet
            var query = dbContext.Products.AsQueryable();

            // Get only available products
            query = query.Where(item => item.Available == true);

            // Search by name
            if (!string.IsNullOrEmpty(name))
                query = query.Where(item => item.ProductName.ToLower().Contains(name.ToLower()));

            return query;
        }

        public static async Task<Product> GetProductAsync(this StoreDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ProductID == entity.ProductID);

        public static async Task<Product> GetProductByProductNameAsync(this StoreDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ProductName == entity.ProductName);

        public static async Task<ProductLike> GetProductLikeByProductIDAndCreationUserAsync(this StoreDbContext dbContext, int? productID, string creationUser)
            => await dbContext.ProductLikes.FirstOrDefaultAsync(item => item.ProductID == productID && item.CreationUser == creationUser);

        public static async Task<ProductPriceHistory> GetProductPriceHistoryAsync(this StoreDbContext dbContext, ProductPriceHistory entity)
            => await dbContext.ProductPriceHistory.FirstOrDefaultAsync(item => item.ProductPriceHistoryID == entity.ProductPriceHistoryID);

        public static IQueryable<OrderDetail> GetOrderDetails(this StoreDbContext dbContext, int? orderHeaderID = null, int? productID = null)
        {
            // Get query from DbSet
            var query = dbContext.OrderDetails.AsQueryable();

            // Filter by: 'OrderHeaderID'
            if (orderHeaderID.HasValue)
                query = query.Where(item => item.OrderHeaderID == orderHeaderID);

            // Filter by: 'ProductID'
            if (productID.HasValue)
                query = query.Where(item => item.ProductID == productID);

            return query;
        }
    }
}
